<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="HotelEdit.aspx.cs" Inherits="HotelEdit" Title="Hotel Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Hotel - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="HotelId" runat="server" DataSourceID="HotelDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/HotelFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/HotelFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Hotel not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:HotelDataSource ID="HotelDataSource" runat="server"
			SelectMethod="GetByHotelId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="HotelId" QueryStringField="HotelId" Type="String" />

			</Parameters>
		</data:HotelDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewServicio1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewServicio1_SelectedIndexChanged"			 			 
			DataSourceID="ServicioDataSource1"
			DataKeyNames="ServicioId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Servicio.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]" />				
				<asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="[Precio]" />				
				<asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="[Moneda]" />				
				<asp:BoundField DataField="Iva" HeaderText="Iva" SortExpression="[Iva]" />				
				<asp:BoundField DataField="Alicuota" HeaderText="Alicuota" SortExpression="[Alicuota]" />				
				<asp:BoundField DataField="Validez" HeaderText="Validez" SortExpression="[Validez]" />				
				<asp:BoundField DataField="VisibilidadTarifa" HeaderText="Visibilidad Tarifa" SortExpression="[VisibilidadTarifa]" />				
				<data:HyperLinkField HeaderText="Proveedor Id" DataNavigateUrlFormatString="ProveedorEdit.aspx?ProveedorId={0}" DataNavigateUrlFields="ProveedorId" DataContainer="ProveedorIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Transporte Id" DataNavigateUrlFormatString="TransporteEdit.aspx?TransporteId={0}" DataNavigateUrlFields="TransporteId" DataContainer="TransporteIdSource" DataTextField="NroCoche" />
				<data:HyperLinkField HeaderText="Hotel Id" DataNavigateUrlFormatString="HotelEdit.aspx?HotelId={0}" DataNavigateUrlFields="HotelId" DataContainer="HotelIdSource" DataTextField="Nombre" />
				<asp:BoundField DataField="TipoServicio" HeaderText="Tipo Servicio" SortExpression="[TipoServicio]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Servicio Found! </b>
				<asp:HyperLink runat="server" ID="hypServicio" NavigateUrl="~/admin/ServicioEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ServicioDataSource ID="ServicioDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ServicioProperty Name="Hotel"/> 
					<data:ServicioProperty Name="Proveedor"/> 
					<data:ServicioProperty Name="Transporte"/> 
					<%--<data:ServicioProperty Name="PaqueteServicioCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ServicioFilter  Column="HotelId" QueryStringField="HotelId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ServicioDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewHabitacion2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewHabitacion2_SelectedIndexChanged"			 			 
			DataSourceID="HabitacionDataSource2"
			DataKeyNames="HabitacionId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Habitacion.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="NroHabitacion" HeaderText="Nro Habitacion" SortExpression="[NroHabitacion]" />				
				<asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="[Tipo]" />				
				<data:HyperLinkField HeaderText="Hotel Id" DataNavigateUrlFormatString="HotelEdit.aspx?HotelId={0}" DataNavigateUrlFields="HotelId" DataContainer="HotelIdSource" DataTextField="Nombre" />
				<asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="[Estado]" />				
				<asp:BoundField DataField="Capacidad" HeaderText="Capacidad" SortExpression="[Capacidad]" />				
				<asp:BoundField DataField="Ocupacion" HeaderText="Ocupacion" SortExpression="[Ocupacion]" />				
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="[Nombre]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Habitacion Found! </b>
				<asp:HyperLink runat="server" ID="hypHabitacion" NavigateUrl="~/admin/HabitacionEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:HabitacionDataSource ID="HabitacionDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:HabitacionProperty Name="Hotel"/> 
					<%--<data:HabitacionProperty Name="ReservaHabitacionCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:HabitacionFilter  Column="HotelId" QueryStringField="HotelId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:HabitacionDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewViajeHotel3" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewViajeHotel3_SelectedIndexChanged"			 			 
			DataSourceID="ViajeHotelDataSource3"
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
		
		<data:ViajeHotelDataSource ID="ViajeHotelDataSource3" runat="server" SelectMethod="Find"
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
						<data:ViajeHotelFilter  Column="HotelId" QueryStringField="HotelId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ViajeHotelDataSource>		
		
		<br />
		

</asp:Content>

