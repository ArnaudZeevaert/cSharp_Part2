   M S B A M L   `   `   `  ����N  JPersonalMap_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PersonalMap_ManagerPersonalMap_Manager  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   )    #PersonalMap_Manager.FenetrePolyline   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:PersonalMap_Manager     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   -   5          � Title$  
fenetrePOI��5	   	    � AllowsTransparency .  � WindowStyle$
 None=�5
   #    � 
ResizeMode$ NoResize=�5   	    � SizeToContent$ WidthAndHeight=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   
���   � � 	Resources 5         _ListBoxItemStyle �� 
TargetType �� Property	 �� Value
 �� Triggers P� Property P� Value :� RoutedEvent g� Duration �� TargetProperty	 W� To��%k  &	        �� 5   
   " ��6*   .�5      ��  �@J�Opacity$		 0.5Q�6(   �� 5       � ��$	 75��6*   
 5      P� 5       �@��
IsSelected .  �5      �� 5       �@J�Opacity$		 1.0Q�64   5      :� 5      $ Mouse.MouseEnter��6   [�5      �� 5      .��5      �� .��5   "   W� $ 0:0:0.2D�5      $ 	MaxHeight�5      $ 90X�5      5       5!      5#      :� 5$      $ Mouse.MouseLeave��6   [�5%      �� 5&      .��5'      �� .��5(   "   W� $ 0:0:1D�5)      $ 	MaxHeight�5*      5+      5,      5.      50      51      .��53      �� ._�54   
   �  � RowDefinitions 55      �� 56      $��48 �6    6.   �� 57      $
��auto �6    60   �� 58      $
��auto �6    60   �� 59      $��40 �6    6.   .+�5<      ]� $��0��6   .+�5=      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�66   .+�5>      �� $!��./Ressources/trajetIcon.png��6   6C   �� 5?      $��Polyline��6   $��Consolas)�6/   $��19(�6E   6U   5@      5A      n� 5B      -   5        $��0��5B       J� Opacity$ 0Q�6!   
������ ��6F   Z� 6\   ]� 5C      $��0��6   .+�5D      �� $ 
Horizontal=�6   $��Right=�66   .+�5E      �� -   5        $��32��5E      $��32��6(    � ToolTip$3 -annuler les changements et fermer la fenêtre��5F      ��5G      �� 5H       �� ImageSource$ ./Ressources/croix.png��6)   5J      5K      5L      A� 5N      $��1��6    A� Rows$ 1��6'    A� Columns$ 2��60   .+�5O      �� $��0��6   .+�5P      �� $
��ID :��6   $��Unispace)�6+   $��22(�6A   $��Center=�6O   �� 5Q      $��Nom trajet:��6   $��Unispace)�62   $��22(�6H   $��Center=�6V   �� 5R      $��Couleur:��6   $��Unispace)�6/   $��22(�6E   $��Center=�6S   �� 5S      $��
Epaisseur:��6   $��Unispace)�61   $��22(�6G   $��Center=�6U   5T      �� 5U      $��1��6   .+�5V      �� -   5         ��Name$ idLabel��5V      $�� ��6-   $��30��5W       $
��auto��6/   $��Unispace)�5X       $��22(�66   $��Center=�5Y       �� 5Z      -   5         ��Name$ TextBoxTrajet��5Z      $�� ��66   
������ 
���   �$��1q�5\   6   $��Unispace)�5]   "   $��20(�68   $��Center=�5^   "   $��Center=�5_   "   
�������6D   �� 5`      -   5         ��Name$ ComboBoxCouleurs��5`      $��30��5b   #   $��Center=�5c   #   $��Unispace)�5d   #   $��18(�69   
������ 
�������
�������$��1q�5f   I    �� SelectedItem 5a   #   �� *	Couleur+5f   I   �� 5g      -   5        $ TextBoxEpaisseur��5g      $�� ��68   
������ 
���   �$��1q�5i   6   $��Unispace)�5j   "   $��20(�68   $��Center=�5k   "   $��Center=�5l   "   
�������$��50��6V   5m      5n      A� 5p      $��2��6   $ 1��6'   $ 2��60   .+�5q      �� $��0��6   .+�5r      �� $��Liste de Coordonnées:��6   $��Unispace)�6<   $��22(�6R   $��Center=�6`   �� 5s      -   5         ��Name$ ModifierCoordonnee��5s      $��50��6Z   $��50��6f   $, &Modifier la coordonnée sélectionnée��6q   ��5t      �� 5u      $ ./Ressources/edit.png��6)   5w      �� 5x      -	   5        $ SupprimerCoordonnee��5x      $��50��6\   $��50��6h   $- 'Supprimer la coordonnée sélectionnée��6s   ��5y      �� 5z      $ ./Ressources/delete.png��6)   5|      �� 5}      -
   5        $ AjouterCoordonnee��5}      $��50��6X   $��50��6d   $. (Ajouter une coordonnée à la collection��6o   ��5~      �� 5      $ ./Ressources/add.png��6)   5�      �� 5�      -   5        $ DetailsCoordonnee��5�      $��50��6X   $��50��6d   $Q KAfficher les caractéristiques complètes de la coordonnées sélectionnée��6o   ��5�      �� 5�      $ ./Ressources/details.png��6)   5�      5�      �� 5�      $��1��6   .+�5�      �� -   5         ��Name$ ListBox��5�      
���   �$	��250��5�   &   $	��200��63   $
��Auto=�5�   &   $
��Auto=�5�   &   #���   5�   &   5�      5�      A� 5�      $��3��6   $ 1��6'   .+�5�      �� -   5        $ ButtonAnnuler��5�      $��0��5�      $5 /annuler les changements sans fermer la fenêtre��5�      $��Stretch=�5�      $��Stretch=�5�      $��Annuler��5�      
���  ��
���   �$��2q�5�   -   $��14(�5�      $��Unispace)�6'   $��Center=�5�      $��Center=�5�      �� 5�      -   5        $ ButtonAppliquer��5�      $��1��5�      $7 1appliquer les changements sans fermer la fenêtre��5�      $��Stretch=�5�      $��Stretch=�5�      $��	Appliquer��5�      
��� � �
���   �$��2q�5�   -   $��14(�5�      $��Unispace)�6'   $��Center=�5�      $��Center=�5�      �� 5�      -   5        $ ButtunOk��5�      $��2��5�      $5 /appliquer les changements et fermer la fenêtre��5�      $��Stretch=�5�      $��Stretch=�5�      $��OK��5�      
������ 
���   �$��2q�5�   -   $��14(�5�      $��Unispace)�6'   $��Center=�5�      $��Center=�5�      
���   �6;   5�      5�      5�      5�      