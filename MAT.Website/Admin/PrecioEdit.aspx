<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PrecioEdit.aspx.cs" Inherits="PrecioEdit" Title="Precio Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Precio - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PrecioId" runat="server" DataSourceID="PrecioDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PrecioFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PrecioFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Precio not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PrecioDataSource ID="PrecioDataSource" runat="server"
			SelectMethod="GetByPrecioId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PrecioId" QueryStringField="PrecioId" Type="String" />

			</Parameters>
		</data:PrecioDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPaquetePrecio1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaquetePrecio1_SelectedIndexChanged"			 			 
			DataSourceID="PaquetePrecioDataSource1"
			DataKeyNames="PaquetePrecioId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PaquetePrecio.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Precio Id" DataNavigateUrlFormatString="PrecioEdit.aspx?PrecioId={0}" DataNavigateUrlFields="PrecioId" DataContainer="PrecioIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Precio Found! </b>
				<asp:HyperLink runat="server" ID="hypPaquetePrecio" NavigateUrl="~/admin/PaquetePrecioEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaquetePrecioDataSource ID="PaquetePrecioDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaquetePrecioProperty Name="Paquete"/> 
					<data:PaquetePrecioProperty Name="Precio"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaquetePrecioFilter  Column="PrecioId" QueryStringField="PrecioId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaquetePrecioDataSource>		
		
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
						<data:PasajeFilter  Column="PrecioId" QueryStringField="PrecioId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PasajeDataSource>		
		
		<br />
		

</asp:Content>

