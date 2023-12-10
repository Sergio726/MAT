<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ViajeEdit.aspx.cs" Inherits="ViajeEdit" Title="Viaje Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Viaje - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="ViajeId" runat="server" DataSourceID="ViajeDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ViajeFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/ViajeFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Viaje not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:ViajeDataSource ID="ViajeDataSource" runat="server"
			SelectMethod="GetByViajeId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="ViajeId" QueryStringField="ViajeId" Type="String" />

			</Parameters>
		</data:ViajeDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPasaje1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPasaje1_SelectedIndexChanged"			 			 
			DataSourceID="PasajeDataSource1"
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
		
		<data:PasajeDataSource ID="PasajeDataSource1" runat="server" SelectMethod="Find"
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
						<data:PasajeFilter  Column="ViajeId" QueryStringField="ViajeId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PasajeDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewViajeHotel2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewViajeHotel2_SelectedIndexChanged"			 			 
			DataSourceID="ViajeHotelDataSource2"
			DataKeyNames="ViajeHotelId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_ViajeHotel.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Viaje Id" DataNavigateUrlFormatString="ViajeEdit.aspx?ViajeId={0}" DataNavigateUrlFields="ViajeId" DataContainer="ViajeIdSource" DataTextField="Origen" />
				<data:HyperLinkField HeaderText="Hotel Id" DataNavigateUrlFormatString="HotelEdit.aspx?HotelId={0}" DataNavigateUrlFields="HotelId" DataContainer="HotelIdSource" DataTextField="Nombre" />
				<asp:BoundField DataField="Desde" HeaderText="Desde" SortExpression="[Desde]" />				
				<asp:BoundField DataField="Hasta" HeaderText="Hasta" SortExpression="[Hasta]" />				
				<asp:BoundField DataField="HoraIngreso" HeaderText="Hora Ingreso" SortExpression="[HoraIngreso]" />				
				<asp:BoundField DataField="HoraSalida" HeaderText="Hora Salida" SortExpression="[HoraSalida]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Viaje Hotel Found! </b>
				<asp:HyperLink runat="server" ID="hypViajeHotel" NavigateUrl="~/admin/ViajeHotelEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ViajeHotelDataSource ID="ViajeHotelDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ViajeHotelProperty Name="Hotel"/> 
					<data:ViajeHotelProperty Name="Viaje"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ViajeHotelFilter  Column="ViajeId" QueryStringField="ViajeId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ViajeHotelDataSource>		
		
		<br />
		

</asp:Content>

