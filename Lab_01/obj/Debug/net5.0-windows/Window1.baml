   M S B A M L   `   `   `  ����A  =Lab_01, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab_01Lab_01  5   	       Lab_01.Window1   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Lab_01     5   	     � WindowStartupLocation$  CenterScreen=�5   "    � Title$! Student data writer/remover��5   	   $	��450��6-   $	��800��6:   .��5	      � -   5        $��0,0,0,-6q�5	      ��5
   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ Black��6    � Offset$ 1Q�6-   � 5      $ 	#FF00D6FF��6   $ 0Q�61   5      .+�5   
   �� -   5        $��Back to main menu��5      $
��Left=�6-   $��37��6H   $��
46,345,0,0q�6T   $	��Top=�6h   $	��187��6�   $��16(�6�   ���	#FF00191E
�������6�   �� 5   
   -   5         ��Name$ 	Exit_butt��5      $
��Exit��6$   $
��Left=�63   $��37��6N   $��585,345,0,0q�6Z   $	��Top=�6o   $	��164��6�   $��16(�6�   ���	#FF00191E
�������6�   �� 5   
   -   5         ��Name$ f_name��5      $
��Left=�6"   $��34��6=   $��
25,120,0,0q�6I   $��
First name��6]    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   $	��208��6�   ���	#FF7FDEFA$��18(�6�   	 �� SelectionOpacity$		 0.3Q�6�   $y�Visible=�6�   ${�False��6�   �� 5   
   -   5        $ l_name��5      $
��Left=�6"   $��33��6=   $��
25,187,0,0q�6I   $��	Last name��6]   $
 Wrap=�6n   $	��Top=�6�   $	��208��6�   ���	#FF7FDEFA$��18(�6�   $y�Visible=�6�   ${�False��6�   �� 5   
   -   5        $ stud_id��5      $
��Left=�6#   $��33��6>   $��	25,61,0,0q�6J   $��
Student ID��6]   $
 Wrap=�6o   $	��Top=�6�   $	��208��6�   ���	#FF7FDEFA$��18(�6�   $y�Visible=�6�   ${�False��6�   �� 5   
   -   5        $ write_remove��5      $��Write to a file��6'   $
��Left=�6A   $��66��6\   $��344,137,0,0q�6h   $	��Top=�6}   $	��167��6�   ���	#FF64DBFF$��16(�6�   ���	#FF4BC3CA${�False��6�   6�   �� 5   
   -   5        
 ��Name$
 
combo_data��5      $
��Left=�6'   $��28��6B   $��
344,78,0,0q�6N   $	��Top=�6b   $	��167��6z   .@�5      �� $��Add student data��6   �� 5      $��Remove data(with ID)��6   5      5      5      