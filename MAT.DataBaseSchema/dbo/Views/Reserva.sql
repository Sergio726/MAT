CREATE VIEW [dbo].[Reserva]
AS
SELECT        dbo.Pasaje.PasajeID, dbo.Pasaje.FechaReserva, dbo.Persona.Apellido, dbo.Persona.Nombre, dbo.Persona.NroDocumento, dbo.Pasaje.ViajeID, 
                         P.Descripcion AS Paquete, dbo.Pasaje.FacturaID,
                             (SELECT        C.ClienteID
                               FROM            dbo.Cliente AS C INNER JOIN
                                                         dbo.CuentaCorriente AS CC ON C.ClienteID = CC.ClienteID INNER JOIN
                                                         dbo.MovimientoCuenta AS MV ON CC.CuentaCorrienteID = MV.CuentaCorrienteID INNER JOIN
                                                         dbo.Factura AS Fac ON Fac.FacturaID = MV.FacturaID
                               WHERE        (Fac.FacturaID = dbo.Pasaje.FacturaID)) AS ClienteID,
                             (SELECT        C.TipoID
                               FROM            dbo.Cliente AS C INNER JOIN
                                                         dbo.CuentaCorriente AS CC ON C.ClienteID = CC.ClienteID INNER JOIN
                                                         dbo.MovimientoCuenta AS MV ON CC.CuentaCorrienteID = MV.CuentaCorrienteID INNER JOIN
                                                         dbo.Factura AS Fac ON Fac.FacturaID = MV.FacturaID
                               WHERE        (Fac.FacturaID = dbo.Pasaje.FacturaID)) AS TipoCliente
FROM            dbo.Persona INNER JOIN
                         dbo.Pasajero ON dbo.Persona.PersonaID = dbo.Pasajero.PasajeroID INNER JOIN
                         dbo.Pasaje ON dbo.Pasajero.PasajeroID = dbo.Pasaje.PasajeroID INNER JOIN
                         dbo.Viaje AS V ON V.ViajeID = dbo.Pasaje.ViajeID INNER JOIN
                         dbo.Paquete AS P ON V.PaqueteID = P.PaqueteID
				
			

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Reserva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'nd
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Reserva';








GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[54] 4[20] 2[10] 3) )"
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
         Top = -288
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Persona"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Pasajero"
            Begin Extent = 
               Top = 77
               Left = 341
               Bottom = 206
               Right = 550
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Pasaje"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 399
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "V"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 531
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "P"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 663
               Right = 247
            End
            DisplayFlags = 280
            TopColumn = 0
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
      E', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Reserva';



