   M S B A M L   `   `   `  ����D  @ClientGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientGUI	ClientGUI  5   	       ClientGUI.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClientGUI     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��880��6)    � 	Resources 5	       �� 
TargetType �� Property �� Value��%>  '��       '�� H     '�� �     �� 5
   
   " ��6   .�5      ��  � ��$ 14(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 14(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$	 150��6&   �� 5       � ��$ 5q�6'   �� 5       � ��$ 50��6'   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��0.3* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 	BooksGrid��5      $��0��6&      AutoGenerateColumns$ False��63      HorizontalGridLinesBrush
 �����	   RowBackground
	 �����
   AlternatingRowBackground

 �����$��5q�5   D   ��5   	   �� *Books+ �� SelectedItem 6'   �� *SelectedBook+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$	 №��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 60   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   ��  �� Path$ Id�  5!      $ Название��6%   $	 180 67    6C   �� $
 Name�  5"      $ 
Автор��6%   $	 175 65    6A   �� $ Autor�  5#      $ Жанр��6%   $	 150 63    6?   �� $ Genre�  5$      $ Год выпуска��6%   $ 90 6:    6E   �� $
 Year�  5%      $ Цена��6%   $ 50 63    6>   �� $ Price�4  .System.Windows.Controls.DataGridCheckBoxColumn  5&      $, &Наличие в библиотеке��6)   $ * 6G    6Q   �� $ IsAvaliable�5(      �� 5*   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��	175 0 0 0q�6;   .+�5+      �� ��6   �� *LoadFromDBCommand+.��6;   -+Загрузить с Базы данных6T   �� 5,      ��6   �� *DeleteCommand+.��67   Удалить6@   �� 5-      ��6   �� *SaveToDBCommand+.��69   Сохранить6D   5.      50      51      