   M S B A M L   `   `   `  ����N  JPersonalMap_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PersonalMap_ManagerPersonalMap_Manager  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   '    !PersonalMap_Manager.FenetreOption   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:PersonalMap_Manager     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$  FenetreOption��5	   	    � SizeToContent$ WidthAndHeight=�5
   	    J� SnapsToDevicePixels .  � AllowsTransparency .  � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�5   	   
���   �.��5      �� ._�5   
   �  � RowDefinitions 5      �� 5      $��48 �6    6.   �� 5      $
��auto �6    60   �� 5      $��48 �6    6.   .+�5      ]� $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�66   .+�5      �� $"��./Ressources/settingIcon.png��6   6D   �� 5      $��Options��6   $��Consolas)�6.   $��19(�6D   6T   5      5      n� 5      -   5        $��0��5      	 J� Opacity$	 0Q�6!   
������ ��6F   Z� 6\   ]� 5      $��0��6   .+�5      �� $ 
Horizontal=�6   $��Right=�66   .+�5       �� -   5        $��32��5       $��32��6(   
 � ToolTip$3
 -annuler les changements et fermer la fenêtre��5!      ��5"      �� 5#       �� ImageSource$ ./Ressources/croix.png��6)   5%      5&      5'      A� 5)      $��1��6    A� Rows$ 1��6'    A� Columns$ 2��60   .+�5*      �� $��0��6   .+�5+      �� $��Couleur de fond :��6   $��30��69   $��Unispace)�6E   $��22(�6[   $��Center=�6i   �� 5,      $��Couleur du text :��6   $��30��69   $��Unispace)�6E   $��22(�6[   $��Center=�6i   �� 5-      $��Dossier de travail :��6   $��30��6;   $��Unispace)�6G   $��22(�6]   $��Center=�6k   �� 5.      -   5         ��Name$ ModifierDossierDeTravail��5.      $��50��6f   $��50��6r   $$
 Modifier le dossier de travail��6}   ��5/      �� 50      $ ./Ressources/edit.png��6)   52      53      �� 55      $��1��6   .+�56      �� -   5         ��Name$ couleurDeFondCombo��56      $��30��58   #   $��Center=�59   #   $��Unispace)�5:   #   $��18(�69   
������ 
�������
�������$��1q�5<   I    �� SelectedItem 57   #   �� *CouleurFondSelectionne+5<   I   �� 5=      -   5        $ couleurDeTextCombo��5=      $��30��5?   #   $��Center=�5@   #   $��Unispace)�5A   #   $��18(�69   
������ 
�������
�������$��1q�5C   I    5>   #   �� *CouleurTextSelectionne+5C   I   �� 5D      -   5         ��Name$ dossierDeTravailLabel��5D      $��30��6;   $�� ��6G   $��Unispace)�6R   $��22(�6h   $��Center=�6v   5F      5G      A� 5I      $��2��6   $ 1��6'   .+�5J      �� -   5        $ ButtonAnnuler��5J      $��0��5K      $5
 /annuler les changements sans fermer la fenêtre��5L      $��Stretch=�5M      $��Stretch=�5N      $��Annuler��5P      
���  ��
���   �$��2q�5R   -   $��14(�5S      $��Unispace)�6'   $��Center=�5T      $��Center=�5U      �� 5V      -   5        $ ButtonAppliquer��5V      $��1��5W      $7
 1appliquer les changements sans fermer la fenêtre��5X      $��Stretch=�5Y      $��Stretch=�5Z      $��	Appliquer��5\      
��� � �
���   �$��2q�5^   -   $��14(�5_      $��Unispace)�6'   $��Center=�5`      $��Center=�5a      �� 5b      -	   5        $ ButtunOk��5b      $��2��5c      $5
 /appliquer les changements et fermer la fenêtre��5d      $��Stretch=�5e      $��Stretch=�5f      $��OK��5h      
������ 
���   �$��2q�5j   -   $��14(�5k      $��Unispace)�6'   $��Center=�5l      $��Center=�5m      
���   �6;   5n      5o      5p      5q      