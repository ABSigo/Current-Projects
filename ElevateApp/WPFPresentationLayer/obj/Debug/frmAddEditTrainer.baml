   M S B A M L   `   `   `  ����  WPFPresentationLayer,    &WPFPresentationLayer.frmAddEditTrainer   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Add New Member��5   	   $��Georgia)�6    $	��450��65   $	��650��6B   ���#DDD.��5      � ���#DDD$��	0 0 100 0q�6    � RowDefinitions 5   
   �� 5      �� 5      �� 5	      �� 5
      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��1��6   $��0��6   $��Trainer ID:��6-   $��Right=�6C   $��Center=�6_   �� 5   
   $��2��6   $��0��6   $��First Name:��6-   $��Right=�6C   $��Center=�6_   �� 5   
   $��3��6   $��0��6   $��
Last Name:��6-   $��Right=�6B   $��Center=�6^   �� 5   
   $��4��6   $��0��6   $��Phone Number:��6-   $��Right=�6E   $��Center=�6a   �� 5   
   $��5��6   $��1��6   $��Status:��6-   $��Center=�6?   $��Center=�6\   �� 5   
   -   5         ��Name$ txtTrainerID��5      $��1��6   $��Center=�67   $��1��6R   $��3q�6b   �� 5   
   -   5        $ txtFirstName��5      $��2��6   $��Center=�65   $��1��6P   $��3q�6`   �� 5   
   -   5        $ txtLastName��5      $��3��6   $��Center=�64   $��1��6O   $��3q�6_   �� 5    
   -   5        $ txtPhoneNumber��5       $��4��6   $��Center=�67   $��1��6R   $��3q�6b   �� 5!   
   -   5         ��Name$ 	chkStatus��5!       $��5��6   $��2��63   $��3q�6C   $��Center=�6N   $��Center=�6k   �� 5#   
   $��1��6   $��2��6   $��Pole Level:��6-   $��Right=�6C   $��Center=�6_   �� 5$   
   $��2��6   $��2��6   $��Acro Level:��6-   $��Right=�6C   $��Center=�6_   �� 5%   
   $��3��6   $��2��6   $��Silks Level:��6-   $��Right=�6D   $��Center=�6`   �� 5&   
   $��4��6   $��2��6   $��Lyra Level:��6-   $��Right=�6C   $��Center=�6_   �� 5'   
   -   5         ��Name$ cboPoleLevel��5'       $��1��6   $��Center=�66   $��3��6Q   $��3q�6a   �� 5(   
   -   5        $ cboAcroLevel��5(       $��2��6   $��Center=�67   $��3��6R   $��3q�6b   �� 5)   
   -	   5        $ cboSilksLevel��5)       $��3��6   $��Center=�67   $��3��6R   $��3q�6b   �� 5*   
   -
   5        $ cboLyraLevel��5*       $��4��6   $��Center=�66   $��3��6Q   $��3q�6a   �� 5+   
   -   5         ��Name$ 
btnConfirm��5+      $��2��6%   $��7��65   $��	25 10 5 0q�6B   $��Confirm��6U   6g   �� 5,   
   -   5        $ 	btnCancel��5,      $��3��6$   $��7��64   $��	5 10 25 0q�6A   $��Cancel��6T   6e   5-      5.      