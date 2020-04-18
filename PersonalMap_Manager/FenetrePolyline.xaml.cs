using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using MyCartographyObj;
using System.Drawing;
using Color = System.Windows.Media.Color;
using Brush = System.Windows.Media.Brush;
using ColorConverter = System.Windows.Media.ColorConverter;
using Polyline = MyCartographyObj.Polyline;

namespace PersonalMap_Manager
{
	/// <summary>
	/// Interaction logic for FenetrePolyline.xaml
	/// </summary>
	public partial class FenetrePolyline : Window, INotifyPropertyChanged
    {
		#region CONSTRUCTEURS
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//remplissage des possibilités de couleur de la comboBox
			foreach (PropertyInfo property in typeof(System.Drawing.Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
			{
				if (property.PropertyType == typeof(System.Drawing.Color))
					ComboBoxCouleurs.Items.Add(property.Name);
			}

			PolylineActuel = new Polyline();
			PolylineActuel.Id = _polylineDepart.Id;
			PolylineActuel.NomTrajet = _polylineDepart.NomTrajet;
			PolylineActuel.CollectionDeCoordonnes = new List<Coordonnees>(_polylineDepart.CollectionDeCoordonnes);
			PolylineActuel.Epaisseur = _polylineDepart.Epaisseur;
			PolylineActuel.Couleur = _polylineDepart.Couleur;

			NewPolyline = null;

			Couleur = FenetrePrincipale.GetColorName(PolylineActuel.Couleur);

			UpdateAffichage();
		}
		public FenetrePolyline(Polyline newPolyline)
		{
			InitializeComponent();
			_polylineDepart = newPolyline;
			DataContext = this;
		}
        #endregion

        #region VARIABLES MEMEBRES
        private Polyline _polylineDepart;
		private Polyline _polylineActuel;
		public Polyline NewPolyline;
		private String _couleur;
        #endregion


        #region PROPRIETES
        public Polyline PolylineActuel
		{
			set
			{
				if (value != _polylineActuel)
				{
					_polylineActuel = value;
				}
			}
			get { return _polylineActuel; }
		}
		public string Couleur
		{
			set
			{
				_couleur = value;
				OnPropertyChanged();
			}
			get
			{
				return _couleur;
			}
		}
		#endregion


		#region METHODE
		private void MainHeaderThumb_OnDragDelta(object sender, DragDeltaEventArgs e)
		{
			Left = Left + e.HorizontalChange;
			Top = Top + e.VerticalChange;
		}
		private void UpdatePolylineActuel()
		{
			PolylineActuel.NomTrajet = TextBoxTrajet.Text;

			Color color = Colors.Black;

			try
			{

				color = (Color)ColorConverter.ConvertFromString(Couleur);
			}
			catch (Exception e)
			{
				MessageBox.Show("Erreur encodage de la couleur :\n" + e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
			}			

			PolylineActuel.Couleur = color;

			Brush brush = new SolidColorBrush(PolylineActuel.Couleur);
			Console.WriteLine("DEBUG brush obtenu après conversion : " + brush.ToString());

			try
			{
				PolylineActuel.Epaisseur = Convert.ToInt32(TextBoxEpaisseur.Text);
			}
			catch (Exception e)
			{
				MessageBox.Show("Erreur encodage de l'épaisseur :\n" + e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
				PolylineActuel.Epaisseur = _polylineDepart.Epaisseur;
			}
		}

		private void UpdateListBox()
		{
			ListBox.Items.Clear();
			Console.WriteLine("DEBUG collection tostring:\n" + PolylineActuel.Draw());
			foreach (CartoObj o in PolylineActuel.CollectionDeCoordonnes)
			{
				Coordonnees c = o as Coordonnees;
				ListBox.Items.Add(c);
				Console.WriteLine("DEBUG ceci est une Coordonnee : " + c.Draw());
			}
		}
		private void UpdateAffichage()
		{
			UpdateListBox();

			idLabel.Content = PolylineActuel.Id;
			TextBoxTrajet.Text = PolylineActuel.NomTrajet;			
			ComboBoxCouleurs.SelectedItem = FenetrePrincipale.GetColorName(PolylineActuel.Couleur);			
			TextBoxEpaisseur.Text = PolylineActuel.Epaisseur.ToString();
		}
		#endregion

		#region CLIQUES
		private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
		{
			NewPolyline = null;
			Close();
		}

		private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
		{
			PolylineActuel = _polylineDepart;
			Couleur = FenetrePrincipale.GetColorName(PolylineActuel.Couleur);
			UpdateAffichage();
		}

		private void ButtonAppliquer_Click(object sender, RoutedEventArgs e)
		{
			UpdatePolylineActuel();
			NewPolyline = PolylineActuel;
		}
		private void ButtunOk_Click(object sender, RoutedEventArgs e)
		{
			UpdatePolylineActuel();
			NewPolyline = PolylineActuel;

			Close();
		}
		private void ModifierCoordonnee_Click(object sender, RoutedEventArgs e)
		{
			bool modificationOK = false;
			POI p = (POI)ListBox.SelectedItem;
			if (p == null)
			{
				modificationOK = true;
				MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				fenetrePOI fenetrePOI = new fenetrePOI(p);
				fenetrePOI.Owner = this;
				fenetrePOI.ShowDialog();
				if (fenetrePOI.NewPOI != null)
				{
					foreach (CartoObj oInCollection in PolylineActuel.CollectionDeCoordonnes)
					{
						if (oInCollection is POI)
						{
							POI poiInCollection = oInCollection as POI;
							if (poiInCollection.Id == p.Id)
							{
								if (PolylineActuel.CollectionDeCoordonnes.Remove(poiInCollection))
								{
									POI newPOI = new POI();
									newPOI.Id = p.Id;
									newPOI.Latitude = fenetrePOI.NewPOI.Latitude;
									newPOI.Longitude = fenetrePOI.NewPOI.Longitude;
									newPOI.Description = fenetrePOI.NewPOI.Description;
									Console.WriteLine("DEBUG newPoi to string : " + newPOI.Draw());
									PolylineActuel.CollectionDeCoordonnes.Add(newPOI);
									modificationOK = true;
								}
								break;
							}
						}
					}
				}
				else modificationOK = true;

				UpdateListBox();
			}

			if (!modificationOK)
			{
				MessageBox.Show("La modification à échouée", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void SupprimerCoordonnee_Click(object sender, RoutedEventArgs e)
		{
			bool suppressionOK = false;
			POI p = (POI)ListBox.SelectedItem;
			if (p == null)
			{
				suppressionOK = true;
				MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				foreach (CartoObj oInCollection in PolylineActuel.CollectionDeCoordonnes)
				{
					if (oInCollection is POI)
					{
						POI poiInCollection = oInCollection as POI;
						if (poiInCollection.Id == p.Id)
						{
							if (PolylineActuel.CollectionDeCoordonnes.Remove(poiInCollection))
							{
								MessageBox.Show("Suppression du POI OK", "", MessageBoxButton.OK, MessageBoxImage.Information);
								suppressionOK = true;
							}
							break;
						}
					}
				}

				UpdateListBox();
			}

			if (!suppressionOK)
				MessageBox.Show("La suppresion à échouée", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		private void AjouterCoordonnee_Click(object sender, RoutedEventArgs e)
		{
			bool modificationOK = false;

			POI p = new POI();

			fenetrePOI fenetrePOI = new fenetrePOI(p);
			fenetrePOI.Owner = this;
			fenetrePOI.ShowDialog();
			if (fenetrePOI.NewPOI != null)
			{
				p.Id = p.Id;
				p.Latitude = fenetrePOI.NewPOI.Latitude;
				p.Longitude = fenetrePOI.NewPOI.Longitude;
				p.Description = fenetrePOI.NewPOI.Description;
				Console.WriteLine("DEBUG newPoi to string : " + p.Draw());
				PolylineActuel.CollectionDeCoordonnes.Add(p);
				modificationOK = true;
				UpdateListBox();
			}

			if (!modificationOK)
			{
				MessageBox.Show("L'ajout à échouée", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void DetailsCoordonnee_Click(object sender, RoutedEventArgs e)
		{
			POI p = (POI)ListBox.SelectedItem;
			if (p == null)
			{
				MessageBox.Show("Aucun élément de la ListBox est sélectionné", "ERREUR", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				MessageBox.Show(p.Draw(), "Caractéristiques détaillées du POI sélectionné", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
		#endregion

		#region EVENT
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
