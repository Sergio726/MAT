CREATE VIEW [dbo].[PersonaCliente]
AS
SELECT        dbo.Persona.PersonaID, dbo.Persona.Apellido, dbo.Persona.Nombre, dbo.Persona.NroDocumento, dbo.Persona.Telefono, dbo.Persona.Email, dbo.Persona.FechaNacimiento, dbo.Persona.Domicilio, 
                         dbo.Persona.Sexo, dbo.Persona.LocalidadID, dbo.Cliente.ClienteID, dbo.Cliente.RazonSocial, dbo.Cliente.Cuit, dbo.Cliente.Moneda, dbo.Cliente.Empresa, dbo.Cliente.Ocupacion, dbo.Cliente.FormaPago, 
                         dbo.Cliente.CondicionIva, dbo.Cliente.VendedorID, dbo.Cliente.Fax, dbo.Cliente.Web, dbo.Cliente.Idioma, dbo.Cliente.Promotor, dbo.Cliente.Observacion, dbo.Cliente.TipoID, dbo.Persona.TipoDocumento, 
                         dbo.Persona.Celular, dbo.Persona.Nacionalidad, dbo.Persona.PaisResidencia, dbo.Persona.Provincia
FROM            dbo.Persona INNER JOIN
                         dbo.Cliente ON dbo.Persona.PersonaID = dbo.Cliente.ClienteID


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Persona"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 168
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 11
         End
         Begin Table = "Cliente"
            Begin Extent = 
               Top = 24
               Left = 470
               Bottom = 213
               Right = 679
            End
            DisplayFlags = 280
            TopColumn = 8
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'PersonaCliente';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'PersonaCliente';

