using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyCartographyObj;

namespace PersonalMap_Manager
{
    /// <summary>
    /// Interaction logic for fenetrePOI.xaml
    /// </summary>
    public partial class fenetrePOI : Window
    {
		private POI _poiDepart;
		private POI _poiActuel;
		public POI NewPOI;

		public POI PoiActuel
		{
			set
			{
				if (value != _poiActuel)
				{
					_poiActuel = value;					
				}
			}
			get { return _poiActuel; }
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			PoiActuel = new POI();
			PoiActuel.Id = _poiDepart.Id;
			PoiActuel.Latitude = _poiDepart.Latitude;
			PoiActuel.Longitude = _poiDepart.Longitude;
			PoiActuel.Description = _poiDepart.Description;

			NewPOI = null;

			idLabel.Content = PoiActuel.Id;
			TextBoxDescription.Text = PoiActuel.Description;
			TextBoxLatitude.Text = PoiActuel.Latitude.ToString();
			TextBoxLongitude.Text = PoiActuel.Longitude.ToString();
		}


		public fenetrePOI()
		{
			InitializeComponent();
			_poiDepart = new POI();
		}

		public fenetrePOI(POI pOIdepart)
		{
			InitializeComponent();
			_poiDepart = pOIdepart;
		}
		private void MainHeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
		{
			Left = Left + e.HorizontalChange;
			Top = Top + e.VerticalChange;
		}
		private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
		{
			NewPOI = null;
			Close();
		}

		private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
		{
			PoiActuel = _poiDepart;

			TextBoxDescription.Text = PoiActuel.Description;
			TextBoxLatitude.Text = PoiActuel.Latitude.ToString();
			TextBoxLongitude.Text = PoiActuel.Longitude.ToString();
		}

		private void ButtonAppliquer_Click(object sender, RoutedEventArgs e)
		{
			UpdataPoiActuel();
			NewPOI = PoiActuel;
		}

		private void ButtunOk_Click(object sender, RoutedEventArgs e)
		{
			UpdataPoiActuel();	
			NewPOI = PoiActuel;

			Close();
		}

		private void UpdataPoiActuel()
		{
			try
			{
				PoiActuel.Latitude = Convert.ToDouble(TextBoxLatitude.Text);
			}
			catch(Exception e)
			{
				MessageBox.Show("Erreur encodage de latitude :\n" + e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
				PoiActuel.Latitude = _poiDepart.Latitude;
			}

			try
			{
				PoiActuel.Longitude = Convert.ToDouble(TextBoxLongitude.Text);
			}
			catch (Exception e)
			{
				MessageBox.Show("Erreur encodage de longitude :\n" + e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
				PoiActuel.Longitude = _poiDepart.Longitude;
			}
			
			PoiActuel.Description = TextBoxDescription.Text;
		}

		//public event PropertyChangedEventHandler PropertyChanged;
		//private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		//{
		//	if (PropertyChanged != null)
		//	{
		//		PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		//	}
		//}

		
	}
}
