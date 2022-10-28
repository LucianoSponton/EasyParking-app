using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace EasyParking.Views.Generales
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupItemImage
    {
        public PopupItemImage(byte[] byteImagen)
        {
            InitializeComponent();

            imageItem.SetToolbarItemVisibility("text,save,path,shape,transform,effects,redo,undo", false);

            imageItem.Source = ImageSource.FromStream(() => //
            {                                                    // Convierte de array de bytes a source imagen
                return new MemoryStream(byteImagen);             //
            });                                                  // 


        }
    }
}
