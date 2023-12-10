<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="LocalidadEdit.aspx.cs" Inherits="LocalidadEdit" Title="Localidad Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Localidad - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="Id" runat="server" DataSourceID="LocalidadDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/LocalidadFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/LocalidadFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Localidad not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:LocalidadDataSource ID="LocalidadDataSource" runat="server"
			SelectMethod="GetById"
		>
			<Parameters>
				<asp:QueryStringParameter Name="Id" QueryStringField="Id" Type="String" />

			</Parameters>
		</data:LocalidadDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewHotel1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewHotel1_SelectedIndexChanged"			 			 
			DataSourceID="HotelDataSource1"
			DataKeyNames="HotelId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Hotel.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="[Nombre]" />				
				<asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="[Direccion]" />				
				<asp:BoundField DataField="Cp" HeaderText="Cp" SortExpression="[CP]" />				
				<asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="[Telefono]" />				
				<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="[Email]" />				
				<asp:BoundField DataField="Contacto" HeaderText="Contacto" SortExpression="[Contacto]" />				
				<asp:BoundField DataField="CantidadHabitaciones" HeaderText="Cantidad Habitaciones" SortExpression="[CantidadHabitaciones]" />				
				<asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="[Categoria]" />				
				<asp:BoundField DataField="Child1" HeaderText="Child1" SortExpression="[Child1]" />				
				<asp:BoundField DataField="Child2" HeaderText="Child2" SortExpression="[Child2]" />				
				<asp:BoundField DataField="ChildHabitacion" HeaderText="Child Habitacion" SortExpression="[ChildHabitacion]" />				
				<asp:BoundField DataField="CheckIn" HeaderText="Check In" SortExpression="[CheckIn]" />				
				<asp:BoundField DataField="CheckOut" HeaderText="Check Out" SortExpression="[CheckOut]" />				
				<asp:BoundField DataField="GoogleMapHtml" HeaderText="Google Map Html" SortExpression="[GoogleMapHtml]" />				
				<data:HyperLinkField HeaderText="Localidad Id" DataNavigateUrlFormatString="LocalidadEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="LocalidadIdSource" DataTextField="IdDepartamento" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Hotel Found! </b>
				<asp:HyperLink runat="server" ID="hypHotel" NavigateUrl="~/admin/HotelEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:HotelDataSource ID="HotelDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:HotelProperty Name="Localidad"/> 
					<%--<data:HotelProperty Name="ServicioCollection" />--%>
					<%--<data:HotelProperty Name="HabitacionCollection" />--%>
					<%--<data:HotelProperty Name="ViajeHotelCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:HotelFilter  Column="LocalidadId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:HotelDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewPaquete2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPaquete2_SelectedIndexChanged"			 			 
			DataSourceID="PaqueteDataSource2"
			DataKeyNames="PaqueteId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_Paquete.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]" />				
				<asp:BoundField DataField="PrecioCama" HeaderText="Precio Cama" SortExpression="[PrecioCama]" />				
				<asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="[Moneda]" />				
				<asp:BoundField DataField="Iva" HeaderText="Iva" SortExpression="[Iva]" />				
				<asp:BoundField DataField="Alicuota" HeaderText="Alicuota" SortExpression="[Alicuota]" />				
				<asp:BoundField DataField="Temporada" HeaderText="Temporada" SortExpression="[Temporada]" />				
				<asp:BoundField DataField="Cotizacion" HeaderText="Cotizacion" SortExpression="[Cotizacion]" />				
				<asp:BoundField DataField="Codigo" HeaderText="Codigo" SortExpression="[Codigo]" />				
				<data:HyperLinkField HeaderText="Destino Id" DataNavigateUrlFormatString="LocalidadEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="DestinoIdSource" DataTextField="IdDepartamento" />
				<asp:BoundField DataField="PrecioSemiCama" HeaderText="Precio Semi Cama" SortExpression="[PrecioSemiCama]" />				
				<asp:BoundField DataField="Foto" HeaderText="Foto" SortExpression="[Foto]" />				
				<asp:BoundField DataField="ServiciosParticulares" HeaderText="Servicios Particulares" SortExpression="[ServiciosParticulares]" />				
				<asp:BoundField DataField="FechaCreacion" HeaderText="Fecha Creacion" SortExpression="[FechaCreacion]" />				
				<asp:BoundField DataField="PublicWeb" HeaderText="Public Web" SortExpression="[PublicWeb]" />				
				<asp:BoundField DataField="LastUpdate" HeaderText="Last Update" SortExpression="[LastUpdate]" />				
				<asp:BoundField DataField="ModePublicity" HeaderText="Mode Publicity" SortExpression="[ModePublicity]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Paquete Found! </b>
				<asp:HyperLink runat="server" ID="hypPaquete" NavigateUrl="~/admin/PaqueteEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PaqueteDataSource ID="PaqueteDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteProperty Name="Localidad"/> 
					<%--<data:PaqueteProperty Name="PaquetePrecioCollection" />--%>
					<%--<data:PaqueteProperty Name="PaqueteAdicionalCollection" />--%>
					<%--<data:PaqueteProperty Name="PaqueteExcursionCollection" />--%>
					<%--<data:PaqueteProperty Name="ViajeCollection" />--%>
					<%--<data:PaqueteProperty Name="PaqueteServicioCollection" />--%>
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PaqueteFilter  Column="DestinoId" QueryStringField="Id" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PaqueteDataSource>		
		
		<br />
		

</asp:Content>

