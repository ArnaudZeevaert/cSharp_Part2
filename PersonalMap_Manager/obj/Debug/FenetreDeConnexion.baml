   M S B A M L   `   `   `  ����N  JPersonalMap_Manager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PersonalMap_ManagerPersonalMap_Manager  5   	   ,    &PersonalMap_Manager.FenetreDeConnexion   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:PersonalMap_Manager     5   	   U QMaterialDesignThemes.Wpf, Version=3.0.1.920, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Login��5	   	   $	��500��6   $	��350��6$    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   $��Segoe UI Emoji)�5   	   .��5      � .+�5   
   �� $	��280��6   $	��Top=�6!   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF2281D1��6#   � 5      $ 	#FF34268A��6#    � Offset$ 1Q�65   � 5      $ 	#FF33288B��6#   $ 0.546Q�65   5      5      �� 5   
   $��./Ressources/logoGPS.png��6   $	��100��62   $	��Top=�6?   $��0,0,0.4,400q�6W   �� 5   
   $	��280��6   $	��250��6    $��Bottom=�6-   $��0,100q�6H    �� RadiusY$ 10��6W   	 �� RadiusX$	 10��6d   
�������
 J� Effect
 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$ 0Q�63      RenderingBias$ Quality=�6A      ShadowDepth$ 1Q�6Y      Color$ 	#FFBBBBBB��6i   5      � 5    
   $��Bottom=�6   $��35,100q�6*   $	��240��6:   .+�5!      �� $��LOGIN��6   $��Center=�6$   $	��Top=�6A   $��5q�6Y   
�������$��18(�6v   �� 5"      $��Center=�6   $��15q�64   .+�5#      �� -   5         ��Name$ NOM_TextBox��5#      $
��0,10q�6/   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$	 NOM��6=    %  !MaterialDesignFloatingHintTextBox#��[  6b   $��Champagne & Limousines)�6�   $��18(�6�   �� 5$      -   5        $ PRENOM_TextBox��5$      $
��0,10q�62   $ PRENOM��6@   #��[  6h   $��Champagne & Limousines)�6�   $��18(�6�   �� 5%      -   5        $ EMAIL_TextBox��5%      $
��0,10q�61   $ EMAIL��6?   #��[  6f   $��Champagne & Limousines)�6�   $��18(�6�   5&      5'      �� 5)   
   -   5         ��Name$ LOGIN_Button��5)      $	��150��6'   $��Center=�63   $��Bottom=�6P   $
��0,85q�6k   $��LOGIN��6y   6�   �� 5*   
   $��CLIQUER ICI POUR QUITTER��6   $��Center=�64   $��Bottom=�6Q   $��30q�6l   
������� � Cursor$
 Hand��6�   �� 5+   
   -   5        $ QUITTER_Button��5+      $��Center=�6)   $��Bottom=�6F    J� Opacity$ 0Q�6a   $��101,0,100.4,30q�6m   $	��149��6�   $��16��6�   6�   5,      5-      