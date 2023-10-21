using Microsoft.EntityFrameworkCore;

namespace AdministradorSeries;

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
        InitializeComponent();
        Conexion(CadenaConexion);
        CargarContenido();
        SelectCBox("Todos");
        // DatosP = _GeneroRepository.Get().ToList();
        // comboBox.DataSource = DatosP;
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

    //Funcion que carga los componentes del formulario
    private void CargarContenido()
    {
        _GeneroRepository = new GeneroRepository(_context);
        this.MaximizeBox = false; // Deshabilita el botón de Maximizar
        this.FormBorderStyle = FormBorderStyle.FixedSingle;

        crearTextBox(textBoxTitulo);
        CreateCheckBox();
        CreateComboBox();
        CrearBotones();
    }

    private void crearTextBox(TextBox textBox)
    {
        textBox.Width = 100;
        textBox.Location = new Point(startX + 60, startY);
        textBox.Font = new Font("Arial", 11, FontStyle.Regular);
        this.Controls.Add(textBox);
        CreateLavel(startX, startY, "Titulo:");
        startY += textBox.Height + 20;
    }

    private void CreateCheckBox()
    {
        CheckBox.Text = "Apto para todo publico(ATP)";
        CheckBox.AutoSize = true;
        CheckBox.ForeColor = Color.White;
        CheckBox.Font = new Font("Arial", 10, FontStyle.Regular);
        CheckBox.Location = new Point(startX, startY);

        CheckBox.Checked = false;
        this.Controls.Add(CheckBox);
        startY += 40;
    }
    private void CreateComboBox()
    {
        comboBox.Location = new Point(startX + 60, startY);
        comboBox.Size = new Size(100, 260);
        comboBox.DisplayMember = "Nombre";
        comboBox.ValueMember = "Id";

        this.Controls.Add(comboBox);

        CreateLavel(startX, startY, "Genero:");
        startY += comboBox.Height + 20;
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
                miBoton.Location = new Point(startX, startY);
                miBoton.Click += new EventHandler(Enviar_Click);
            }
            else
            {
                miBoton.Location = new Point(startX + 110, startY);
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
        CargarData = new SerieFiltroDto();
        CargarData.Titulo = textBoxTitulo.Text;
        CargarData.ATP = CheckBox.Checked;
        var genero = (Genero)comboBox.SelectedItem;
        CargarData.Genero = genero.Nombre;
        this.Close();
    }
    private void Conexion(string Cadena)
    {
        var opciones = new DbContextOptionsBuilder<DataContext>()
            .UseNpgsql(Cadena)
            .Options;
        _context = new DataContext(opciones);

    }

}
