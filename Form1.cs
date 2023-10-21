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
    private void Close_Click(object sender, EventArgs e)
    {
        this.Close();
    }

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
}
