   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5      F BContactsApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ContactsApp.ViewsContactsApp.Views 5      ?$clr-namespace:ContactsApp.ConvertersContactsApp.Converters 5      $   ContactsApp.Views.ContactsList   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      1localclr-namespace:ContactsApp.Views      5      5conv$clr-namespace:ContactsApp.Converters      5        J� SnapsToDevicePixels  . $��0q�5   )    � 	Resources 5      1 +ContactsApp.Converters.FirstLetterConverter   firstLetterConverter  cvs �� Source �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName �� GroupDescriptions2  ,System.Windows.Data.PropertyGroupDescription   PropertyName   	Converter��%�   &	        &	      7     5   
   6$   �� 5   
    6+   �� *ContactList+ 5        5      $ FullName��6&    5        5      $ FullName��6+   8   6C   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5       $��1* �6   �� 5!      $��1* �6   �� 5"      $��1* �6   �� 5#      $��1* �6   �� 5$      $��1* �6   �� 5%      $��1* �6   	 � RowDefinitions	 5'   
   �� 5(      $��1* �6   .+�5+   
   �� $��0��6   $��12��6#   $��0��5,      $��0q�5.      ��5-      �� 
 �� Source#
 [  �� 
GroupStyle 5/      (  "System.Windows.Controls.GroupStyle  50         HeaderTemplate 51      �� 52      .R�53      �� $��Stretch=�6%   
���   �$��0,0,0,1q�54   9   ._�55   "   �� $��24(�6,   
���   �$��8,0,0,0q�56   ?   ��55   :   ��  �� Path$
 Name�56   ?   57      58         ContainerStyle 5:      �� 5;       �� 
TargetType" ��6    .�5<      ��  �� Property � �� �� Value
 �����6;   �� 5=       � ��
 �����6;   5>      5@      ��5B      �� 5C      .R�5D      ��  �� Orientation$ 
Horizontal=�6!   .+�5E      �� �� 5F      
���   ���6$   �� *
FullName+6>   5G      5H      5J      5K      5L      