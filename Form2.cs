using Microsoft.EntityFrameworkCore;

namespace AdministradorSeries;

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

    // Redimecionar imagenes de los botones
    private void sizeImagen(Button Boton, string path)
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
        catch (System.Exception)
        {


        }

    }
    //Crear Label
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

    //Caturar Datos del dataGridView
    private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            CapturarData = (SerieDto)dataGridView.Rows[e.RowIndex].DataBoundItem;
        }

    }

    //Establecer conexion mediante la cadena que enviamos desde Form1
    private DataContext Conexion(string Cadena)
    {

        var opciones = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(Cadena)
            .Options;
        return new DataContext(opciones);
    }

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

    //Carga datos en dataGridView
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
    //Crea botones
    private void CrearBotones()
    {
        string[] btnName = { "Nuevo", "Modificar", "Anular", "Eliminar", "Consulta", "Salir", "Prev", "Next" };
        string[] imgName = { "create.png", "edit.png", "unavailable.png", "remove.png", "reserch.png", "logout.png", "back.png", "next.png" };

        //recordar sacar el forech del metodo
        foreach (Button miBoton in new[] { Nuevo, Modificar, Anular, Eliminar, Consulta, Salir, Prev, Next })
        {
            // Establecer propiedades del botón
            miBoton.Text = $"  {btnName[indice]}"; // Texto que se muestra en el botón
            miBoton.Width = 120; // Ancho del botón en píxeles
            miBoton.Height = 40; // Altura del botón en píxeles 

            miBoton.ForeColor = Color.White;
            miBoton.Font = new Font("Arial", 11, FontStyle.Regular);
            miBoton.FlatStyle = FlatStyle.Popup;
            miBoton.FlatAppearance.BorderColor = Color.FromArgb(41, 184, 240); // Color del borde
            miBoton.FlatAppearance.BorderSize = 1;

            sizeImagen(miBoton, $"iconos/{imgName[indice]}");
            if (indice < 4)
                miBoton.Location = new Point(20 + (indice * 100) + 25 * indice, Height - 90);

            this.Controls.Add(miBoton);
            indice++;
            switch (indice)
            {
                case 1:
                    miBoton.Click += new EventHandler(New_Click);
                    continue;
                case 2:
                    miBoton.Click += new EventHandler(Update_Click);
                    continue;
                case 3:
                    miBoton.Click += new EventHandler(Anular_Click);
                    continue;
                case 4:
                    miBoton.Click += new EventHandler(Eliminar_Click);
                    continue;
                case 5:
                    miBoton.Location = new Point(Width - 150, 10);
                    miBoton.Click += new EventHandler(Buscar_Click);
                    continue;
                case 6:
                    miBoton.Location = new Point(Width - 150, Height - 90);
                    miBoton.Click += new EventHandler(Close_Click);
                    continue;
                case 7:
                    miBoton.Location = new Point(Width - 410, 10);
                    miBoton.Click += new EventHandler(Back_Click);
                    continue;
                case 8:
                    miBoton.Location = new Point(Width - 280, 10);
                    miBoton.Click += new EventHandler(Next_Click);
                    continue;
                default:
                    continue;
            }
        }
    }
    //Crea dataGridView
    public void CrearDGV()
    {
        dataGridView.Size = new Size(800, 300); // Ajustar según el tamaño deseado
        dataGridView.Location = new Point((this.ClientSize.Width - dataGridView.Width) / 2, 60);
        dataGridView.BackgroundColor = Color.White;
        //dataGridView.Columns[0].Visible = false;

        this.Controls.Add(dataGridView);
        dataGridView.CellClick += dataGridView_CellClick;
    }

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

    //Diferentes Eventos de los botones
    private void Close_Click(object sender, EventArgs e)
    {
        //this.Close();
        Application.Exit();
    }
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

    private void Form2_FormClosing(object sender, FormClosingEventArgs e)
    {
        // Cierra toda la aplicación cuando se cierra el formulario 2
        Application.Exit();
    }


}
