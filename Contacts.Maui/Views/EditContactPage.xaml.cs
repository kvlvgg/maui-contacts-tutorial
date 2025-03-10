using Contacts.Maui.Models;
using Contact = Contacts.Maui.Models.Contact;

namespace Contacts.Maui.Views;

[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    private Contact? contact;

    public EditContactPage()
    {
        InitializeComponent();
    }

    public string ContactId
    {
        set
        {
            contact = ContactRepository.GetContactById(int.Parse(value));
            if (contact == null) return;

            entryName.Text = contact.Name;
            entryEmail.Text = contact.Email;
            entryPhone.Text = contact.Phone;
            entryAddress.Text = contact.Address;
        }
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {
        if(nameValidator.IsNotValid)
        {
            DisplayAlert("Error", "Name is required.", "OK");
            return;
        }

        if(emailValidator.IsNotValid)
        {
            foreach(var error in emailValidator.Errors)
            {
                DisplayAlert("Error", error.ToString(), "OK");
            }

            return;
        }

        if (contact == null) return;

        contact.Name = entryName.Text;
        contact.Address = entryAddress.Text;
        contact.Email = entryEmail.Text;
        contact.Phone = entryPhone.Text;

        ContactRepository.UpdateContact(contact.ContactId, contact);
        Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }
}