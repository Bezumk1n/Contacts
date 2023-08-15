namespace Contacts.MAUI.Pages;

public partial class ContactsV : ContentPage
{
	public ContactsV(object context)
	{
		InitializeComponent();
		BindingContext = context;
	}
}