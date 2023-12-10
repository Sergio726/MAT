<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="FacturaEdit.aspx.cs" Inherits="FacturaEdit" Title="Factura Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Factura - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="FacturaId" runat="server" DataSourceID="FacturaDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/FacturaFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/FacturaFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Factura not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:FacturaDataSource ID="FacturaDataSource" runat="server"
			SelectMethod="GetByFacturaId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="FacturaId" QueryStringField="FacturaId" Type="String" />

			</Parameters>
		</data:FacturaDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewMovimientoCuenta1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewMovimientoCuenta1_SelectedIndexChanged"			 			 
			DataSourceID="MovimientoCuentaDataSource1"
			DataKeyNames="MovimientoId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_MovimientoCuenta.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Pago Id" DataNavigateUrlFormatString="PagoEdit.aspx?PagoId={0}" DataNavigateUrlFields="PagoId" DataContainer="PagoIdSource" DataTextField="FechaPago" />
				<data:HyperLinkField HeaderText="Factura Id" DataNavigateUrlFormatString="FacturaEdit.aspx?FacturaId={0}" DataNavigateUrlFields="FacturaId" DataContainer="FacturaIdSource" DataTextField="NroFactura" />
				<asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" SortExpression="[FechaRegistro]" />				
				<data:HyperLinkField HeaderText="Cuenta Id" DataNavigateUrlFormatString="CuentaEdit.aspx?CuentaId={0}" DataNavigateUrlFields="CuentaId" DataContainer="CuentaIdSource" DataTextField="Estado" />
				<data:HyperLinkField HeaderText="Nota Id" DataNavigateUrlFormatString="NotaEdit.aspx?NotaId={0}" DataNavigateUrlFields="NotaId" DataContainer="NotaIdSource" DataTextField="PorcentajeRetencion" />
				<data:HyperLinkField HeaderText="Cuenta Corriente Id" DataNavigateUrlFormatString="CuentaCorrienteEdit.aspx?CuentaCorrienteId={0}" DataNavigateUrlFields="CuentaCorrienteId" DataContainer="CuentaCorrienteIdSource" DataTextField="Fecha" />
				<data:HyperLinkField HeaderText="Debito Id" DataNavigateUrlFormatString="DebitoEdit.aspx?DebitoId={0}" DataNavigateUrlFields="DebitoId" DataContainer="DebitoIdSource" DataTextField="Fecha" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Movimiento Cuenta Found! </b>
				<asp:HyperLink runat="server" ID="hypMovimientoCuenta" NavigateUrl="~/admin/MovimientoCuentaEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:MovimientoCuentaDataSource ID="MovimientoCuentaDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:MovimientoCuentaProperty Name="Cuenta"/> 
					<data:MovimientoCuentaProperty Name="CuentaCorriente"/> 
					<data:MovimientoCuentaProperty Name="Debito"/> 
					<data:MovimientoCuentaProperty Name="Factura"/> 
					<data:MovimientoCuentaProperty Name="Nota"/> 
					<data:MovimientoCuentaProperty Name="Pago"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:MovimientoCuentaFilter  Column="FacturaId" QueryStringField="FacturaId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:MovimientoCuentaDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewPasaje2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPasaje2_SelectedIndexChanged"			 			 
			DataSourceID="PasajeDataSource2"
			DataKeyNames="PasajeId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Pasaje.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Pasajero Id" DataNavigateUrlFormatString="PasajeroEdit.aspx?PasajeroId={0}" DataNavigateUrlFields="PasajeroId" DataContainer="PasajeroIdSource" DataTextField="Pasaporte" />
				<data:HyperLinkField HeaderText="Butaca Id" DataNavigateUrlFormatString="ButacaEdit.aspx?ButacaId={0}" DataNavigateUrlFields="ButacaId" DataContainer="ButacaIdSource" DataTextField="NroButaca" />
				<asp:BoundField DataField="FechaReserva" HeaderText="Fecha Reserva" SortExpression="[FechaReserva]" />				
				<asp:BoundField DataField="FechaCompra" HeaderText="Fecha Compra" SortExpression="[FechaCompra]" />				
				<data:HyperLinkField HeaderText="Viaje Id" DataNavigateUrlFormatString="ViajeEdit.aspx?ViajeId={0}" DataNavigateUrlFields="ViajeId" DataContainer="ViajeIdSource" DataTextField="Origen" />
				<data:HyperLinkField HeaderText="Factura Id" DataNavigateUrlFormatString="FacturaEdit.aspx?FacturaId={0}" DataNavigateUrlFields="FacturaId" DataContainer="FacturaIdSource" DataTextField="NroFactura" />
				<data:HyperLinkField HeaderText="Estado Pasaje" DataNavigateUrlFormatString="EstadoPasajeEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="EstadoPasajeSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Voucher Id" DataNavigateUrlFormatString="VoucherEdit.aspx?VoucherId={0}" DataNavigateUrlFields="VoucherId" DataContainer="VoucherIdSource" DataTextField="NroVoucher" />
				<data:HyperLinkField HeaderText="Precio Id" DataNavigateUrlFormatString="PrecioEdit.aspx?PrecioId={0}" DataNavigateUrlFields="PrecioId" DataContainer="PrecioIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Pasaje Found! </b>
				<asp:HyperLink runat="server" ID="hypPasaje" NavigateUrl="~/admin/PasajeEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PasajeDataSource ID="PasajeDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PasajeProperty Name="Butaca"/> 
					<data:PasajeProperty Name="Factura"/> 
					<data:PasajeProperty Name="Pasajero"/> 
					<data:PasajeProperty Name="Precio"/> 
					<data:PasajeProperty Name="Viaje"/> 
					<data:PasajeProperty Name="Voucher"/> 
					<data:PasajeProperty Name="EstadoPasaje"/> 
					<%--<data:PasajeProperty Name="PasajeAdicionalCollection" />--%>
					<%--<data:PasajeProperty Name="ReservaHabitacionCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PasajeFilter  Column="FacturaId" QueryStringField="FacturaId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PasajeDataSource>		
		
		<br />
		

</asp:Content>

