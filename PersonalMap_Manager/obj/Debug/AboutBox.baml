   M S B A M L   `   `   `  ����N  JPersonalMap_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PersonalMap_ManagerPersonalMap_Manager  5   	   "    PersonalMap_Manager.AboutBox   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:PersonalMap_Manager     5   	     � Title$  AboutBox��5   	    � SizeToContent$ WidthAndHeight=�6    J� SnapsToDevicePixels . $	��400��6T    � AllowsTransparency .  � WindowStyle$
 None=�5	   #    � 
ResizeMode$ NoResize=�66    � WindowStartupLocation$ CenterScreen=�6L   
���   �.��5
      �� ._�5   
   �  � RowDefinitions 5      �� 5      $��48 �6    6.   �� 5      $��* �6    6-   �� 5      $
��auto �6    �� 5      $	��100 �6    .+�5      ]� $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�66   .+�5      �� $&�� ./Ressources/informationIcon.png��6   6H   �� 5      $��AboutBox��6   61   5      5      n� 5      -   5        $��0��5      	 J� Opacity$	 0Q�6!   
������ ��6F   Z� 6\   ]� 5      $��0��6   .+�5      �� $ 
Horizontal=�6   $��Right=�66   .+�5      �� -   5        
 � ToolTip$
 Close��5      $��32��6-   $��32��68   ��5      �� 5       �� ImageSource$ ./Ressources/croix.png��6)   5       5!      5"      �� 5#      $��1��6   ._�5$      �� $ Vertical=�6   $
��Left=�64   .+�5%      �� $��20��6!   6/   �� 5&      $��	36,0,36,0q�6!   $ 
Horizontal=�64   .+�5'      �� $��0,0,12,0q�6%   .+�5(      �� .��6$   Operating System:67   �� 5)      .��6$   .Net Framework Version:6=   �� 5*      $��0,12,0,0q�6$   .��66   Windows Username:6I   �� 5+      .��6$   Domainname:61   �� 5,      .��6%   
Processor:61   �� 5-      $��0,12,0,0q�6$   .��66   IP Address (LAN):6I   �� 5.      $��0,28,0,0q�6$   .��66   Date :6>   5/      �� 50      .+�51      �� -   5        $%��SystemInfoWindowOperatingSystem��51   $   .��6K   --6O   �� 52      -   5        $)��#SystemInfoWindowNetFrameworkVersion��52   $   .��6O   ---6T   �� 53      -   5        $%��SystemInfoWindowWindowsUserName��53   $   $��0,12,0,0q�6K   .��6]   ---6b   �� 54      -   5        $ ��SystemInfoWindowDomainName��54   $   .��6F   ---6K   �� 55      -   5        $��SystemInfoWindowProcessor��55   $   .��6E   ---6J   �� 56      -   5        $��SystemInfoWindowLanIp��56   $   $��0,12,0,0q�6A   .��6S   ---6X   �� 57      -	   5        $!��SystemInfoWindowRubyVersion��57   $   $��0,28,0,0q�6G   .��6Y   ---6^   58      59      �� 5:      $��20��6!   6/   5;      5<      �� 5?      $��2��6   $(��"Informations Personne Connectée :��6!   
���   �$��2q�6a   �� 5A      $��3��6   $
��auto=�6(   .��5B      �� -
   5         ��Name$$ TextBlockInfoPersonneConnectee��5B      $	��400��5C      $
��auto��6$   5D      5F      5G      5H      