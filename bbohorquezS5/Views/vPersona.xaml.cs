using bbohorquezS5.Models;

namespace bbohorquezS5.Views;

public partial class vPersona : ContentPage
{
    public vPersona()
    {
        InitializeComponent();

    }

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        App.personRepo.AddNewPerson(txtName.Text);
        lblStatus.Text = App.personRepo.StatusMessage;
        btnObtener_Clicked(sender, e);
        this.txtName.Text = "";
    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        //this.lblStatus.Text = "";
        List<Persona> people = App.personRepo.GetAllPeople();
        listaPersona.ItemsSource = people;
        lblStatus.Text = App.personRepo.StatusMessage;
    }

    private void btnEditar_Clicked(object sender, EventArgs e)
    {
        if (txtName.Text != null)
        {
            Persona selectedPerson = (Persona)listaPersona.SelectedItem;
            App.personRepo.EditPerson(selectedPerson.Id, txtName.Text);
            btnObtener_Clicked(sender, e);
            lblStatus.Text = App.personRepo.StatusMessage;
            this.txtName.Text = "";
        }
        else
        {
            lblStatus.Text = App.personRepo.StatusMessage;
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        Persona selectedPerson = (Persona)listaPersona.SelectedItem;
        App.personRepo.DeletePerson(selectedPerson.Id);
        btnObtener_Clicked(sender, e);
        lblStatus.Text = App.personRepo.StatusMessage;

    }

    private void listaPersona_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Persona selectedPerson = (Persona)listaPersona.SelectedItem;
    }
}

   