   M S B A M L   `   `   `  ����N  JPersonalMap_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PersonalMap_ManagerPersonalMap_Manager  5   	   $    PersonalMap_Manager.fenetrePOI   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:PersonalMap_Manager     5   	   -   5          � Title$  
fenetrePOI��5   	   $	��350��5	   	   $	��350��5
   	    � AllowsTransparency .  � WindowStyle$
 None=�5   #    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ CenterScreen=�6L   
���   �.��5      �� ._�5   
   �  � RowDefinitions 5      �� 5      $��48 �6    6.   �� 5      $��* �6    6-   �� 5      $��40 �6    6.   .+�5      ]� $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�66   .+�5      �� $��./Ressources/poiIcon.png��6   6@   �� 5      $	��POI��6   $��Consolas)�6*   $��19(�6@   6P   5      5      n� 5      -   5        $��0��5       J� Opacity$ 0Q�6!   
������ ��6F   Z� 6\   ]� 5      $��0��6   .+�5      �� $ 
Horizontal=�6   $��Right=�66   .+�5      �� -   5        $��32��5      $��32��6(    � ToolTip$3 -annuler les changements et fermer la fenêtre��5      ��5      �� 5       	 �� ImageSource$	 ./Ressources/croix.png��6)   5"      5#      5$      �� 5%      $��1��6   ._�5&      � 
 � ColumnDefinitions
 5'      �� 5(      $
��auto �6+   �� 5)      $
��auto �6+   .+�5,      �� $��0��6!   $��Center=�61   .+�5-      �� $
��ID :��6    $��Unispace)�6/   $��22(�6E   $��Center=�6S   �� 5.      $��	Latitude:��6    $��Unispace)�64   $��22(�6J   $��Center=�6X   �� 5/      $��
Longitude:��6    $��Unispace)�65   $��22(�6K   $��Center=�6Y   �� 50      $��Description:��6    $��Unispace)�67   $��22(�6M   $��Center=�6[   51      �� 53      $��1��6!   $��Center=�61   .+�54      �� -   5         ��Name$ idLabel��54       $�� ��61   $��30��6<   $
��auto��6K   $��Unispace)�6Y   $��22(�6o   $��Center=�6}   �� 55      -   5         ��Name$ TextBoxLatitude��55   "   $�� ��6<   
������ 
���   �$��1q�6r   $��Unispace)�6�   $��20(�6�   $��Center=�6�   $��Center=�6�   
�������6�   �� 56      -   5        $ TextBoxLongitude��56   "   $�� ��6<   
������ 
���   �$��1q�6q   $��Unispace)�6�   $��20(�6�   $��Center=�6�   $��Center=�6�   
�������6�   �� 57      -   5        $ TextBoxDescription��57   "   $�� ��6>   
������ 
���   �$��1q�6s   $��Unispace)�6�   $��20(�6�   $��Center=�6�   $��Center=�6�   
�������$��50��6  58      59      5:      A� 5<      $��2��6    A� Rows$ 1��6'   .+�5=      �� -   5         ��Name$ ButtonAnnuler��5=      $��0��5>      $5 /annuler les changements sans fermer la fenêtre��5?      $��Stretch=�5@      $��Stretch=�5A      $��Annuler��5C      
���  ��
���   �$��2q�5E   -   $��14(�5F      $��Unispace)�6'   $��Center=�5G      $��Center=�5H      �� 5I      -	   5        $ ButtonAppliquer��5I      $��1��5J      $7 1appliquer les changements sans fermer la fenêtre��5K      $��Stretch=�5L      $��Stretch=�5M      $��	Appliquer��5O      
��� � �
���   �$��2q�5Q   -   $��14(�5R      $��Unispace)�6'   $��Center=�5S      $��Center=�5T      �� 5U      -
   5        $ ButtunOk��5U      $��2��5V      $5 /appliquer les changements et fermer la fenêtre��5W      $��Stretch=�5X      $��Stretch=�5Y      $��OK��5[      
������ 
���   �$��2q�5]   -   $��14(�5^      $��Unispace)�6'   $��Center=�5_      $��Center=�5`      
���   �6;   5a      5e      5f      5i      