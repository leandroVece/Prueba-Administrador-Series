using Microsoft.EntityFrameworkCore;

namespace AdministradorSeries;

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

        switch (indice)
        {
            case 0:
                textBox.Width = 550;
                textBox.Location = new Point(startX + 110, startY);
                break;
            case 1:
                textBox.Multiline = true;
                textBox.Width = 550;
                textBox.Height = 60;
                textBox.Location = new Point(startX + 110, startY);
                break;
            case 2:
                textBox.Width = 100;
                textBox.Location = new Point(startX + 110, startY);
                break;
            case 3:
                textBox.Width = 100;
                textBox.Location = new Point(startX + 110, startY);
                break;
            default:
                break;
        }
        textBox.Font = new Font("Arial", 11, FontStyle.Regular);
        this.Controls.Add(textBox);
        CreateLavel(startX, startY, NamesLabel[indice]);
        startY += textBox.Height + 20;
        indice++;

    }

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
    private void CreateComboBox()
    {
        comboBox.Location = new Point(370, 220);
        comboBox.Size = new Size(100, 260);
        comboBox.DisplayMember = "Nombre";
        comboBox.ValueMember = "Id";

        this.Controls.Add(comboBox);


        CreateLavel(250, 220, "Genero:");
    }

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

    private void CrearBotones()
    {
        string[] btnName = { "Enviar", "Salir" };
        int i = 0;

        foreach (Button miBoton in new[] { Enviar, Salir })
        {
            // Establecer propiedades del botón
            miBoton.Text = btnName[i]; // Texto que se muestra en el botón
            miBoton.Width = 100; // Ancho del botón en píxeles
            miBoton.Height = 30; // Altura del botón en píxeles 
            if (i == 0)
            {
                miBoton.Location = new Point(Width - 140, Height - 80);
                miBoton.Click += new EventHandler(Enviar_Click);
            }
            else
            {
                miBoton.Location = new Point(Width - 250, Height - 80);
                miBoton.Click += new EventHandler(Close_Click);
            }
            i++;

            miBoton.ForeColor = Color.White;
            miBoton.Font = new Font("Arial", 11, FontStyle.Regular);
            miBoton.FlatStyle = FlatStyle.Popup;
            miBoton.FlatAppearance.BorderColor = Color.FromArgb(41, 184, 240); // Color del borde
            miBoton.FlatAppearance.BorderSize = 1;
            this.Controls.Add(miBoton);
        }
    }

    private void Close_Click(object sender, EventArgs e)
    {
        this.Close();
    }
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

    public Boolean TextBoxControl()
    {

        foreach (TextBox textBox in new[] { textBoxTitulo, textBoxDescripcion, textBoxEstrella, textBoxPrecio })
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, complete todos los campos.");
                return false;
            }
        }

        return true;
    }

    private void Conexion(string Cadena)
    {
        var opciones = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(Cadena)
            .Options;
        _context = new DataContext(opciones);

    }

}
