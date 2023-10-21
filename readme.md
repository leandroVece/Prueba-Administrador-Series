# Programa con Windows forms

## Conexion a la base de datos

La creacion de la base de datos se hace por Entity Framewor usando dos modelos que determinaran los valores que tendran.

**Path:./Models/Serie.cs**

    public class Serie
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEstreno { get; set; }
        public int estrellas { get; set; }
        [Precision(14, 2)]
        public decimal PrecioAlquiler { get; set; }
        public bool ATP { get; set; }
        public string Estado { get; set; }
        public Guid GeneroForeiKey { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual Genero Genero { get; set; }
    }

**Path:./Models/Genero.cs**

    public class Genero
    {
        public Guid Id { get; set; }
        [NotMapped]
        [JsonIgnore]
        public Guid SerieForeiKey { get; set; }
        public string Nombre { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual List<Serie> Series { get; set; }

    }

Con estos modelos puedo usar Fuent Api para determinar el tipo de columna y sus caracteristicas especiales en una clase llamada DataContext

**Path:./Helpers/DbContext.cs**

    public class DataContext : DbContext
    {
        public DbSet<Serie> Series { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Serie>(serie =>
            {
                serie.ToTable("Series");
                serie.HasKey(x => x.Id);

                serie.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
                serie.Property(x => x.Estado).IsRequired().HasMaxLength(2);
                serie.Property(x => x.FechaEstreno)
                    .HasColumnType("date");

                serie.HasOne(x => x.Genero).WithMany(x => x.Series).HasForeignKey(x => x.GeneroForeiKey);

                serie.HasData(DatosPruba.Lista());

            });

            modelBuilder.Entity<Genero>(cat =>
            {
                cat.ToTable("Generos");
                cat.HasKey(x => x.Id);

                cat.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
                cat.HasData(DatosPruba.ListaGenero());
            });
        }
        //prueeba la conexion
        public bool TestConnection()
        {
            try
            {
                return Database.CanConnect(); // Verificar si se puede establecer una conexión con la base de datos
            }
            catch (Exception)
            {
                return false; // Si hay alguna excepción, la conexión falló
            }
        }
    }


Aqui estableco la relacion 1:N de Genero y series, la fecha que es de formato "date"(dd/MM/yyyy) y la cantidad maxima que tendra el atributo estado que sera de 2.

En el appsettings dejo la cadena de conexion 

**Path:./appsettings.js**

    {
        "ConnectionStrings": {
            "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=simple;Database=serie;"
        }
    }

Y en el archivo program.cs inyecto el servicio para establecer la conexion a la base de datos y determinando a que base de datos voy a conectarme. En este caso en PostgreSQL.

**Path:./program.cs**


    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configurar servicios y contexto de base de datos
            var serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                    options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=simple;Database=serie;"))
                //.AddScoped<IGeneroRepository, GeneroRepository>()
                //.AddScoped<ISeriesRepository, SerieRepository>()
                .BuildServiceProvider();

            using (var context = serviceProvider.GetRequiredService<DataContext>())
            {
                // Asegurarse de que la base de datos esté creada y las migraciones se hayan aplicado
                context.Database.Migrate();

                Application.Run(new Form1()); // Pasa el contexto de base de datos al formulario principal
            }
        }

    }

Con esto ya podemos Crear la base de datos usando las herramientas que nos proporciona EF.

    dotnet ef migrations add InitailCreate
    dotnet ef database update

Con estos dos comando crearemos las base de datos, las tablas y algunos valores predeterminados que puse en el archivo [datos](./Helpers/Datos.cs)

Porm ultimo creo los servicios para consumir la base de datos.

**Path:./Services/Interfaces/ISeriesRepository.js**

    public interface ISeriesRepository
    {
        IEnumerable<SerieDto> Get(int page);
        IEnumerable<SerieDto> Getfiltro(SerieFiltroDto filtro, int page);
        Task Save(Serie data);
        Task Update(Guid id, Serie data);
        Task Delete(Guid id);
    }

**Path:./Services/SeriesRepository.js**

    public class SerieRepository : ISeriesRepository, IDisposable
    {
        DataContext context;
        public SerieRepository(DataContext dbContext)
        {
            context = dbContext;
        }
        public async Task Delete(Guid id)
        {
            var aux = context.Series.Find(id);
            if (aux != null)
            {
                context.Remove(aux);
                await context.SaveChangesAsync();
            }
        }
        public IEnumerable<SerieDto> Get(int page)
        {
            var response = context.Series.Join(context.Generos, ser => ser.GeneroForeiKey, gen => gen.Id,
            (ser, Gen) => new SerieDto
            {
                Id = ser.Id,
                Titulo = ser.Titulo,
                Genero = Gen.Nombre,
                Descripcion = ser.Descripcion,
                Estrellas = ser.estrellas,
                PrecioAlquiler = ser.PrecioAlquiler,
                FechaEstreno = ser.FechaEstreno,
                Estado = ser.Estado,
                ATP = ser.ATP
            })
            .Skip((page - 1) * 5)
            .Take(5)
            .ToList().OrderBy(x => x.Titulo);

            return response;
        }
        public IEnumerable<SerieDto> Getfiltro(SerieFiltroDto filtro, int page)
        {
            try
            {
                var predicate = PredicateBuilder.New<SerieDto>();
                if (!string.IsNullOrEmpty(filtro.Titulo))
                    predicate = predicate.And(p => p.Titulo.ToLower().Contains(filtro.Titulo.ToLower()));
                //predicate = predicate.And(p => EF.Functions.Like(p.Titulo, $"%{filtro.Titulo}%"));
                if (filtro.Genero != "Todos")
                    predicate = predicate.And(p => p.Genero == filtro.Genero);
                if (filtro.ATP == true)
                    predicate = predicate.And(p => p.ATP == true);



                var response = context.Series.Join(context.Generos, ser => ser.GeneroForeiKey, gen => gen.Id,
                (ser, Gen) => new SerieDto
                {
                    Id = ser.Id,
                    Titulo = ser.Titulo,
                    Genero = Gen.Nombre,
                    Descripcion = ser.Descripcion,
                    Estrellas = ser.estrellas,
                    PrecioAlquiler = ser.PrecioAlquiler,
                    FechaEstreno = ser.FechaEstreno,
                    Estado = ser.Estado,
                    ATP = ser.ATP
                })
                .Where(predicate)
                .Skip((page - 1) * 5)
                .Take(5)
                .ToList().OrderBy(x => x.Titulo);

                return response;
            }
            catch (System.Exception)
            {
                MessageBox.Show("error al cargar");
                throw;
            }
        }

        public async Task Save(Serie data)
        {
            try
            {
                context.Add(data);
                context.SaveChangesAsync();

            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public async Task Update(Guid id, Serie data)
        {
            var aux = context.Series.Find(id);
            if (aux != null)
            {
                aux.Titulo = data.Titulo;
                aux.PrecioAlquiler = data.PrecioAlquiler;
                aux.ATP = data.ATP;
                aux.FechaEstreno = data.FechaEstreno;
                aux.estrellas = aux.estrellas;
                aux.Descripcion = data.Descripcion;
                aux.GeneroForeiKey = data.GeneroForeiKey;

                await context.SaveChangesAsync();
            }
        }
        public async Task UpdateActividad(Guid id, string estado)
        {
            var aux = context.Series.Find(id);
            if (aux != null)
            {
                aux.Estado = estado;
                await context.SaveChangesAsync();
            }
        }
        //Metodo para cerrar conexion una vez usado el Using
        public void Dispose()
        {
            context.Dispose(); // Libera los recursos de DbContext
        }

    }

En este repositorio contamos con las 4 operaciones basicas (CRUD) donde las funciones GET traen registros limitados para mejorar la eficiencia y la experiencia del usuario

**Path:./Services/Interfaces/ISeriesRepository.js**

    public interface IGeneroRepository
    {
        IEnumerable<Genero> Get();
        Genero GetBYName(string Nombre);
        Task Save(Genero data);
        Task Update(Guid id, Genero data);
        Task Delete(Guid id);
    }

**Path:./Services/SeriesRepository.js**

    public class GeneroRepository : IGeneroRepository, IDisposable
    {

        DataContext context;
        public GeneroRepository(DataContext dbContext)
        {
            context = dbContext;
        }
        public async Task Delete(Guid id)
        {
            var aux = context.Generos.Find(id);
            if (aux != null)
            {
                context.Remove(aux);
                await context.SaveChangesAsync();
            }
        }
        public IEnumerable<Genero> Get()
        {
            return context.Generos;
        }
        public Genero GetBYName(string Nombre)
        {
            var response = context.Generos.Where(x => x.Nombre == Nombre).FirstOrDefault();
            return response;
        }

        public async Task Save(Genero data)
        {
            context.Add(data);
            await context.SaveChangesAsync();
        }

        public async Task Update(Guid id, Genero data)
        {
            var aux = context.Generos.Find(id);
            if (aux != null)
            {
                aux.Nombre = data.Nombre;
                await context.SaveChangesAsync();
            }
        }

        //metodo para cerrar la conexion una vez usado el using
        public void Dispose()
        {
            context.Dispose(); // Libera los recursos de DbContext
        }

    }

## Form1

Para el formulario de presentacion vamos a limitar sus tamaño.

**PATH:./Form1.designer.cs**

    partial class Form1
    {
    
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //Establecemos el tamaaño predeterminado de la aplicacion
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Text = "Administracion de Series";

        }
        #endregion
    }

**PATH:./Form1.cs**

En el formulario tenemos vamos a crear 5 textbox que van a enviar una cadena de conexion al Form2 para que podemos conectarnos. Para ello vamos a necesitar Crear el evento y las cajas de texto.

    namespace AdministradorSeries;

    public partial class Form1 : Form
    {
        //variables para posiccion
        int startX = 50;
        int startY = 50;
        int spacingY = 10;
        //incice para recorrer los arrat
        int indice = 0;
        string[] NamesLabel = { "Base de Datos:", "Servidor:", "Puerto:", "Usuario:", "Password:" };
        string[] TextBoxDefault = { "serie", "localhost", "5432", "postgres", "simple" };
        Button miBoton = new Button();
        Button CloseBoton = new Button();
        TextBox textBoxBb = new TextBox();
        TextBox textBoxservidor = new TextBox();
        TextBox textBoxPuerto = new TextBox();
        TextBox textBoxUsuario = new TextBox();
        TextBox textBoxPasword = new TextBox();

        public Form1()
        {
            InitializeComponent();

            this.MaximizeBox = false; // Deshabilita el botón de Maximizar
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(32, 32, 32);

            // Crear un nuevo TextBox
            foreach (TextBox textBox in new[] { textBoxBb, textBoxservidor, textBoxPuerto, textBoxUsuario, textBoxPasword })
            {
                textBox.Width = 200;
                textBox.Text = TextBoxDefault[indice];
                textBox.Location = new Point(startX + 150, startY);
                textBox.Font = new Font("Arial", 11, FontStyle.Regular);

                this.Controls.Add(textBox);
                CreateLavel(startX, startY, NamesLabel[indice]);
                startY += textBox.Height + spacingY;
                indice++;
            }

            // Establecer propiedades del botón
            miBoton.Text = "  Conectar"; // Texto que se muestra en el botón
            miBoton.Width = 120; // Ancho del botón en píxeles
            miBoton.Height = 40; // Altura del botón en píxeles 
            miBoton.Location = new Point(150, startY);

            miBoton.ForeColor = Color.White;
            miBoton.Font = new Font("Arial", 11, FontStyle.Regular);
            miBoton.FlatStyle = FlatStyle.Popup;
            miBoton.FlatAppearance.BorderColor = Color.FromArgb(41, 184, 240); // Color del borde
            miBoton.FlatAppearance.BorderSize = 1; // Ancho del borde
            // miBoton.Image = Image.FromFile("iconos/create.png");
            // miBoton.ImageAlign = ContentAlignment.MiddleLeft;
            // miBoton.TextImageRelation = TextImageRelation.ImageBeforeText;
            sizeImagen(miBoton, "iconos/enter.png");

            CloseBoton.Text = "  Close";
            CloseBoton.Width = 120;
            CloseBoton.Height = 40;
            CloseBoton.Location = new Point(280, startY);
            CloseBoton.ForeColor = Color.White;
            CloseBoton.Font = new Font("Arial", 11, FontStyle.Regular);
            CloseBoton.FlatStyle = FlatStyle.Popup;
            CloseBoton.FlatAppearance.BorderColor = Color.FromArgb(41, 184, 240); // Color del borde
            CloseBoton.FlatAppearance.BorderSize = 1; // Ancho del borde
            sizeImagen(CloseBoton, "iconos/logout.png");

            // Asociar un evento al botón (por ejemplo, el evento Click)
            miBoton.Click += new EventHandler(MiBoton_Click);
            CloseBoton.Click += new EventHandler(Close_Click);

            // Agregar el botón al formulario
            this.Controls.Add(miBoton);
            this.Controls.Add(CloseBoton);
        }

        //Redimenciona las imagenes de los botones
        private async void sizeImagen(Button Boton, string path)
        {
        }

        private void CreateLavel(int x, int y, string nombre)
        {
        }

        private void MiBoton_Click(object sender, EventArgs e)
        {
        }
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //comprueba que todos los textBox esten con datos.
        public Boolean TextBoxControl()
        {
        }
    }

>## Advertencia: no se esta recurriendo a buenas Practica, solo se lo esta dejando de esta manera para fines demostrativos

Como vemos en nuestro codigo tenemos metodos privados. Veamos detenidamente que hace cada uno.

El primero **TextBoxControl()** mantiene el contro de los Texbox para hacegurarse que esten cargados de datos. en caso contrario devolvera un false y un mensaje de advertencia.

    public Boolean TextBoxControl()
    {
        foreach (TextBox textBox in new[] { textBoxBb, textBoxservidor, textBoxPuerto, textBoxUsuario, textBoxPasword })
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, complete todos los campos.");
                return false;
            }
        }
        return true;
    }

El segundo **MiBoton_Click()** cargara los datos en una cadena que se enviara al segundo formulario. Este envento esta controlado por nuestro metodo anterir **TextBoxControl()**


    private void MiBoton_Click(object sender, EventArgs e)
    {
        // Capturar el valor del TextBox
        if (TextBoxControl())
        {
            string CadenaDeConexion = $"Host={textBoxservidor.Text};Username={textBoxUsuario.Text};Password={textBoxPasword.Text};Database={textBoxBb.Text};Port={textBoxPuerto.Text}";
            // Hacer algo con el valor capturado, por ejemplo, mostrarlo en un MessageBox

            Form2 form2 = new Form2(CadenaDeConexion);
            form2.Show();
            this.Hide();
        }
    }

Tercero es para la creacion de Label junto a los botones. Estos recibiran un valor x, un valor y y el texto que tendran.

    private void CreateLavel(int x, int y, string nombre)
    {
        Label label = new Label();
        label.Text = nombre; // Texto que se muestra en el Label
        label.Location = new Point(x, y);
        label.Width = 150;
        label.ForeColor = Color.White;
        label.Font = new Font("Arial", 11, FontStyle.Regular);
        this.Controls.Add(label);
    }

El cuarto metodo se va a encargar de Redimencionar las imagenes. Para adaptarse a los botones. En este caso, le enviamos el boton al que queremos colocar la imagen y la ruta del archivo.

    private async void sizeImagen(Button Boton, string path)
    {
        try
        {
            Image originalImage = Image.FromFile(path);
            int newHeight = Boton.Height - 15;
            Image resizedImage = new Bitmap(originalImage, new Size(newHeight, newHeight));

            Boton.Image = resizedImage;
            Boton.ImageAlign = ContentAlignment.MiddleLeft;
            Boton.TextImageRelation = TextImageRelation.ImageBeforeText;
        }
        catch (System.Exception ex)
        {

            Console.WriteLine(ex.ToString());
        }

    }

## Form2

En el form2 vamos a tener nuestros formulario principal el cual va a contar con un Crud basico, una funcion de paginado por defecto traera 5 solicitdes por peticion y una funcion de busqueda.

En este formulario creamos los componentes por medios de metodos que iremos llamando en en nuestro constructor principal.

**PATH:./Form1.cs**

    public partial class Form2 : Form
    {

        //private SerieRepository _serieRepository;
        int indice = 0;
        //indice de pagiacion para DGV sin filtrar
        int indicePaginacion = 1;
        //indice de pagiacion para DGV filtrando
        string CadenaConexion;

        //panel para que se vea bonito la conexion
        public Panel Panel;

        //Botones
        Button Consulta = new Button();
        Button Nuevo = new Button();
        Button Modificar = new Button();
        Button Anular = new Button();
        Button Eliminar = new Button();
        Button Salir = new Button();
        Button Next = new Button();
        Button Prev = new Button();

        //DataGridWiew
        DataGridView dataGridView = new DataGridView();
        //objeto para capturar los datos del dataGridView
        SerieDto CapturarData { get; set; }

        SerieFiltroDto filtro { get; set; }
        public Form2(string Cadena)
        {

            InitializeComponent();
            this.FormClosing += Form2_FormClosing;
            this.MaximizeBox = false; // Deshabilita el botón de Maximizar
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.FromArgb(32, 32, 32);
            CadenaConexion = Cadena;
            //Conexion(CadenaConexion);
            //_serieRepository = new SerieRepository(_context);
            CrearDGV();
            CrearBotones();
            CargarDatosDGV();
            CrearPanel();
            comprobarConexion();

        }

        private void comprobarConexion()
        {
        }
        // Redimecionar imagenes de los botones
        private void sizeImagen(Button Boton, string path)
        {
            
        }
        //Crear Label
        private void CreateLavel(int x, int y, string nombre)
        {
            
        }

        //Caturar Datos del dataGridView
        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Establecer conexion mediante la cadena que enviamos desde Form1
        private DataContext Conexion(string Cadena)
        {

        }

        //Metodo para confirmar si se puede realizar la accion de modificar/anular/eliminar
        private bool Action()
        {
            
        }

        //Carga datos en dataGridView
        private void CargarDatosDGV()
        {
            
        }
        //Crea botones
        private void CrearBotones()
        {
            
        }
        //Crea dataGridView
        public void CrearDGV()
        {
            
        }

        //Creacion del panel
        private void CrearPanel()
        {
        }

        //Diferentes Eventos de los botones
        private void Close_Click(object sender, EventArgs e)
        {
            
        }
        private void Back_Click(object sender, EventArgs e)
        {
            
        }
        private void Next_Click(object sender, EventArgs e)
        {
        }
        private void New_Click(object sender, EventArgs e)
        {
        
        }
        private void Update_Click(object sender, EventArgs e)
        {

        }

        private void Anular_Click(object sender, EventArgs e)
        {
            
        }
        private void Eliminar_Click(object sender, EventArgs e)
        {
            
        }
        private void Buscar_Click(object sender, EventArgs e)
        {
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

    }

La primera funcion que vamos a describir son las de deconexion. La priemra se recibe la cadena de Conexion() que traemos desde form1. para conectarnos a la base de datos.

La segunda funcion comprobarConexion() es una funcion que nos indicara que la conexion se establecio, en caso contrario nos mostrara un menssagebox que nos indicara que no se establecio la conexion y el cambiara el panel de color.

    private DataContext Conexion(string Cadena)
        {

            var opciones = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql(Cadena)
                .Options;
            return new DataContext(opciones);
        }

    private void comprobarConexion()
    {
        using (var _context = Conexion(CadenaConexion))
        {
            if (_context.TestConnection())
            {
                Panel.BackColor = Color.Lime;
                CreateLavel(45, 20, "Conexion Establecida");
            }
            else
            {
                Panel.BackColor = Color.Red;
                CreateLavel(45, 20, "Fallo en la conexion");
            }
        }
    }

El siguiente metodo crea el panel que nos mostrara luz verde si esta la conexion establecida o luz roja en caso de fallar.

    //Creacion del panel
    private void CrearPanel()
    {
        Panel = new Panel();
        Panel.Size = new Size(15, 15);
        Panel.Location = new Point(25, 20);
        Panel.BackColor = Color.DimGray;
        this.Controls.Add(Panel);

        // Calcula el radio del círculo basado en el ancho y alto del panel
        int radio = Math.Min(Panel.Width, Panel.Height);

        // Crea un rectángulo que servirá como área de recorte
        Rectangle rectangle = new Rectangle(0, 0, radio, radio);

        // Crea una región circular usando el rectángulo como base
        System.Drawing.Drawing2D.GraphicsPath circularPath = new System.Drawing.Drawing2D.GraphicsPath();
        circularPath.AddEllipse(rectangle);
        // Establece la región del panel con la forma circular
        Panel.Region = new Region(circularPath);
    }

Esta funcion verifica si seleccionamos un elemento del DataGridView y verifica si este elemento esta activo o no, en el caso contrario botones como modificar elimnar no funcionaran.

   //Metodo para confirmar si se puede realizar la accion de modificar/anular/eliminar
    private bool Action()
    {
        if (CapturarData == null)
        {
            MessageBox.Show("Seleccione un Elemento");
            return false;
        }
        else
        {
            if (CapturarData.Estado != "AC")
            {
                MessageBox.Show("La serie no se encuentra activa");
                return false;
            }
            else return true;
        }
    }


Los metodos para crear y cargar El dataGridView son los siguientes.

    public void CrearDGV()
    {
        dataGridView.Size = new Size(800, 300); // Ajustar según el tamaño deseado
        dataGridView.Location = new Point((this.ClientSize.Width - dataGridView.Width) / 2, 60);
        dataGridView.BackgroundColor = Color.White;
        //dataGridView.Columns[0].Visible = false;

        this.Controls.Add(dataGridView);
        dataGridView.CellClick += dataGridView_CellClick;
    }

    private void CargarDatosDGV()
    {
        try
        {
            using (var repository = new SerieRepository(Conexion(CadenaConexion)))
            {
                List<SerieDto> data = new List<SerieDto>();
                if (filtro == null)
                    data = repository.Get(indicePaginacion).ToList();
                else
                    data = repository.Getfiltro(filtro, indicePaginacion).ToList();
                dataGridView.DataSource = data;
                dataGridView.Columns[0].Visible = false;
            }
        }
        catch (System.Exception ex)
        {
            MessageBox.Show("Error al cargar datos");
        }
    }


Mientras el priemro se encarga de la creacion el segundo se encarga de cargar los datos verificando si recive los valores del filtro.

El siguiente metodo esta relacionado con los anteriores dos y es el que se encarga de capturar la informacion de nuestro DataGridView para sus posteriores usos.

    private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            CapturarData = (SerieDto)dataGridView.Rows[e.RowIndex].DataBoundItem;
        }

    }

>Nota: Es una buena practica mantener separado la logica que carga los datos en el dataGridView y la de seleccion del metodo para conseguir los datos de la base de datos

Las siguiente funciones van a  cambiar el valor dle indice de paginacion. Este indice es el que le le va a enviar en la fucnion Get() y GetFiltro().

    private void Back_Click(object sender, EventArgs e)
    {
        if (indicePaginacion > 1)
        {
            indicePaginacion--;
            CargarDatosDGV();
        }
    }
    private void Next_Click(object sender, EventArgs e)
    {
        indicePaginacion++;
        CargarDatosDGV();
    }

Las funciones de los eventos Para crear un nuevo elemento y la funcion para actualizarlo tiene algunas cosas en comun. La primera llama a la clase Form3 enviandole solo como parametro la cadena de conexion mientras que la segunda envia un objeto Serie y la cadena.

    private void New_Click(object sender, EventArgs e)
    {
        Form3 form3 = new Form3(CadenaConexion);
        form3.ShowDialog();
        Serie data = form3.CargarData;
        form3.Close();
        if (data != null)
        {
            using (var repository = new SerieRepository(Conexion(CadenaConexion)))
            {
                indicePaginacion = 1;
                repository.Save(data);
                CargarDatosDGV();
            }
        }
        data = null;
    }

    private void Update_Click(object sender, EventArgs e)
    {
        if (Action())
        {
            Form3 form3 = new Form3(CapturarData, CadenaConexion);
            form3.ShowDialog();
            Serie data = form3.CargarData;
            form3.Close();
            if (data != null)
            {
                using (var repository = new SerieRepository(Conexion(CadenaConexion)))
                {
                    indicePaginacion = 1;
                    repository.Update(data.Id, data);
                    CargarDatosDGV();
                }
            }
            data = null;
            CapturarData = null;
        }
    }

Ambas Esperan a recibir un objeto de la clase Form3 y realizar su peticion correspondiente a la base de datos.

El metodo **Anular_Click()** tiene una simulitud al metodo **Update_Click** del boton Modificar ya que se encarga de modificar una sola proiedad.

    private void Anular_Click(object sender, EventArgs e)
    {
        if (Action())
        {
            using (var repository = new SerieRepository(Conexion(CadenaConexion)))
            {
                indicePaginacion = 1;
                repository.UpdateActividad(CapturarData.Id, "IN");
                CargarDatosDGV();
            }
        }
    }

El metodo de busqueda se encarga de habir un cuarto formulario Buscar_Click() y devolver un objeto SerieFiltroDto que cuenta con 3 parametros para realizar una busqueda mas detallada

   private void Buscar_Click(object sender, EventArgs e)
    {
        Form4 form4 = new Form4(CadenaConexion);
        form4.ShowDialog();
        filtro = form4.CargarData;
        if (filtro != null)
        {
            indicePaginacion = 1;
            CargarDatosDGV();
        }
    }

El ultimo metodo es el de eliminar que como su nombre lo dice se encarga de eliminar un registro de nuestra base de datos.

    private void Eliminar_Click(object sender, EventArgs e)
    {
        if (Action())
        {
            using (var repository = new SerieRepository(Conexion(CadenaConexion)))
            {
                indicePaginacion = 1;
                repository.Delete(CapturarData.Id);
                CargarDatosDGV();
            }
        }
    }

El resto de los metodos que son de crear botones y label son lo mismo que hicimos en el form1, pero delegamos la creacion de estos componentes a metodos de la clase.

## Form3

Este se va a encargar de Crear o Actualizar registros. Esta cuenta con muchas funciones que ya teniamos en nuestro Form2 y que reutilizamos en el Form3

    public partial class Form3 : Form
    {
        private DataContext _context;

        //Segundo metodo Propuesto para establecer una conexion...
        private GeneroRepository _GeneroRepository;

        //Botones
        Button Salir = new Button();
        Button Enviar = new Button();

        //objeto para guardar Datos que facilmente podria ser inicializado en el metodo de enviar
        public Serie CargarData { get; set; }

        //Capturar los datos de la base de datos para inicilizarlo en el combobix
        public List<Genero> DatosP { get; set; }

        int indice = 0;
        int startX = 20;
        int startY = 50;
        string[] NamesLabel = { "Titulo", "Descripcion:", "Estrellas:", "Precio Alquiler:" };
        Guid Id { get; set; }

        //botones
        TextBox textBoxTitulo = new TextBox();
        TextBox textBoxDescripcion = new TextBox();
        TextBox textBoxEstrella = new TextBox();
        TextBox textBoxPrecio = new TextBox();

        //chexbox
        CheckBox CheckBox = new CheckBox();

        //calendario
        DateTimePicker Calendario = new DateTimePicker();
        ComboBox comboBox = new ComboBox();

        //Formulario de Carga
        public Form3(string CadenaConexion)
        {
            InitializeComponent();
            Conexion(CadenaConexion);
            CargarContenido();
            DatosP = _GeneroRepository.Get().ToList();
            comboBox.DataSource = DatosP;
        }

        //Formulario para edicion
        public Form3(SerieDto data, string CadenaConexion)
        {
            InitializeComponent();
            Conexion(CadenaConexion);
            CargarContenido();
            textBoxTitulo.Text = data.Titulo;
            textBoxDescripcion.Text = data.Descripcion;
            textBoxEstrella.Text = data.Estrellas.ToString();
            textBoxPrecio.Text = data.PrecioAlquiler.ToString();
            CheckBox.Checked = data.ATP;
            Calendario.Value = data.FechaEstreno;
            //var value = _GeneroRepository.GetBYName(data.Genero);
            SelectCBox(data.Genero);
            Id = data.Id;

        }

        //capturar el genero e inicializarlo en el combobox cuando editamos
        private void SelectCBox(string nombre)
        {
        
        }

        //Funcion que carga los componentes del formulario
        private void CargarContenido()
        {
            _GeneroRepository = new GeneroRepository(_context);
            this.MaximizeBox = false; // Deshabilita el botón de Maximizar
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            foreach (TextBox textBox in new[] { textBoxTitulo, textBoxDescripcion, textBoxEstrella, textBoxPrecio })
            {
                crearTextBox(textBox);
            }
            CrearCalendario();
            CreateCheckBox();
            CreateComboBox();
            CrearBotones();
        }

        private void crearTextBox(TextBox textBox)
        {
            ...
        }

        private void CreateCheckBox()
        {
            
        }
        private void CreateComboBox()
        {
        
        }

        private void CrearCalendario()
        {
        

        }

        //Seleccionar fecha del calendario
        private void miCalendario_Selected(object sender, EventArgs e)
        {
            
        }

        private void CreateLavel(int x, int y, string nombre)
        {
            ...
        }

        private void CrearBotones()
        {
            ...
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Enviar_Click(object sender, EventArgs e)
        {
            
        }

        public Boolean TextBoxControl()
        {
            ...
        }

        private void Conexion(string Cadena)
        {
            ...
        }

    }

Este Formulario cuenta con doble constructor, por lo que para no repetir codigo creamos un metodo llamado Cargar contenido que se va a encargar de crear los contenido. Y dejamos que el contructor inicializara los valores.

    private void CargarContenido()
    {
        _GeneroRepository = new GeneroRepository(_context);
        this.MaximizeBox = false; // Deshabilita el botón de Maximizar
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        foreach (TextBox textBox in new[] { textBoxTitulo, textBoxDescripcion, textBoxEstrella, textBoxPrecio })
        {
            crearTextBox(textBox);
        }
        CrearCalendario();
        CreateCheckBox();
        CreateComboBox();
        CrearBotones();
    }

El metodo **CreateComboBox()** se encarga de crear el componente mientras que el metodo **SelectCBox()** se encargaba de seleccionar el genero que tenia la clase que vamos a modificar.

    private void CreateComboBox()
    {
        comboBox.Location = new Point(370, 220);
        comboBox.Size = new Size(100, 260);
        comboBox.DisplayMember = "Nombre";
        comboBox.ValueMember = "Id";

        this.Controls.Add(comboBox);

        CreateLavel(250, 220, "Genero:");
    }

    
    private void SelectCBox(string nombre)
    {
        DatosP = _GeneroRepository.Get().ToList();
        comboBox.DataSource = DatosP;
        foreach (var item in DatosP)
        {
            if (item.Nombre == nombre)
            {
                comboBox.SelectedItem = item;
                break;
            }
        }
    }

Como se puede apreciar este se encarga de traer los Genero de nuestra base de datos.

El metodo **CreateCheckBox()** como su nombre indica solo se encatga de crear un checkbox.

    private void CreateCheckBox()
    {
        CheckBox.Text = "Apto para todo publico(ATP)";
        CheckBox.AutoSize = true;
        CheckBox.ForeColor = Color.White;
        CheckBox.Font = new Font("Arial", 10, FontStyle.Regular);
        CheckBox.Location = new Point(480, 175);

        CheckBox.Checked = false;
        this.Controls.Add(CheckBox);
    }

La Creacion del elemento para el canlendario y su captura se muestran en las siguientes dos funciones.

    private void CrearCalendario()
    {
        // Configura el formato personalizado para el DateTimePicker
        Calendario.Format = DateTimePickerFormat.Custom;
        Calendario.CustomFormat = "dd/MM/yyyy";
        Calendario.Width = 100;

        Calendario.Location = new Point(370, 173);
        // Agrega el calendario al formulario
        this.Controls.Add(Calendario);

        CreateLavel(250, 175, "fecha de estreno:");

        Calendario.ValueChanged += new EventHandler(miCalendario_Selected);

    }

    //Seleccionar fecha del calendario
    private void miCalendario_Selected(object sender, EventArgs e)
    {
        // Maneja el evento cuando se selecciona una fecha en el DateTimePicker
        DateTimePicker miCalendarioDesplegable = (DateTimePicker)sender;
        //MessageBox.Show("Fecha seleccionada: " + miCalendarioDesplegable.Value.ToShortDateString());
    }

Por ultimo tenemos el boton de enviar que se encarga de inicializar en una variable accesible desde el exterior los valores que se cargaron en el formulario.

    private void Enviar_Click(object sender, EventArgs e)
    {
        if (TextBoxControl())
        {
            CargarData = new Serie();
            CargarData.Id = this.Id;
            CargarData.Titulo = textBoxTitulo.Text;
            CargarData.Descripcion = textBoxDescripcion.Text;
            CargarData.ATP = CheckBox.Checked;
            CargarData.GeneroForeiKey = (Guid)comboBox.SelectedValue;
            CargarData.estrellas = Convert.ToInt32(textBoxEstrella.Text);
            CargarData.FechaEstreno = Calendario.Value;
            CargarData.PrecioAlquiler = decimal.Parse(textBoxPrecio.Text);
            CargarData.Estado = "AC";
            this.Close();
        }
    }

## Form4

El ultimo formulario es una version simplificada del form3 que se encargara de los mismo, solo que se limitara a 3 parametros


    public partial class Form4 : Form
    {
        private DataContext _context;

        //Segundo metodo Propuesto para establecer una conexion...
        private GeneroRepository _GeneroRepository;

        //Botones
        Button Salir = new Button();
        Button Enviar = new Button();

        //objeto para guardar Datos que facilmente podria ser inicializado en el metodo de enviar
        public SerieFiltroDto CargarData { get; set; }

        //Capturar los datos de la base de datos para inicilizarlo en el combobix
        public List<Genero> DatosP { get; set; }

        int startX = 50;
        int startY = 30;

        //botones
        TextBox textBoxTitulo = new TextBox();

        //chexbox
        CheckBox CheckBox = new CheckBox();

        ComboBox comboBox = new ComboBox();

        //Formulario de Carga
        public Form4(string CadenaConexion)
        {
            ...
        }

        private void SelectCBox(string nombre)
        {
            ...
        }

        //Funcion que carga los componentes del formulario
        private void CargarContenido()
        {
           ...
        }

        private void crearTextBox(TextBox textBox)
        {
           ...
        }

        private void CreateCheckBox()
        {
            ...
        }
        private void CreateComboBox()
        {
            ...
        }

        private void CreateLavel(int x, int y, string nombre)
        {
            ...
        }

        private void CrearBotones()
        {
          ...
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Enviar_Click(object sender, EventArgs e)
        {
            ...
        }
        private void Conexion(string Cadena)
        {
            ...
        }
    }
