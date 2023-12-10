<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Hotel.aspx.cs" Inherits="Hotel" Title="Hotel List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Hotel List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="HotelDataSource"
				DataKeyNames="HotelId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Hotel.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="HotelId" HeaderText="Hotel Id" SortExpression="[HotelID]" ReadOnly="True" />
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="[Nombre]"  />
				<asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="[Direccion]"  />
				<asp:BoundField DataField="Cp" HeaderText="Cp" SortExpression="[CP]"  />
				<asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="[Telefono]"  />
				<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="[Email]"  />
				<asp:BoundField DataField="Contacto" HeaderText="Contacto" SortExpression="[Contacto]"  />
				<asp:BoundField DataField="CantidadHabitaciones" HeaderText="Cantidad Habitaciones" SortExpression="[CantidadHabitaciones]"  />
				<asp:BoundField DataField="Categoria" HeaderText="Categoria" SortExpression="[Categoria]"  />
				<asp:BoundField DataField="Child1" HeaderText="Child1" SortExpression="[Child1]"  />
				<asp:BoundField DataField="Child2" HeaderText="Child2" SortExpression="[Child2]"  />
				<asp:BoundField DataField="ChildHabitacion" HeaderText="Child Habitacion" SortExpression="[ChildHabitacion]"  />
				<asp:BoundField DataField="CheckIn" HeaderText="Check In" SortExpression="[CheckIn]"  />
				<asp:BoundField DataField="CheckOut" HeaderText="Check Out" SortExpression="[CheckOut]"  />
				<asp:BoundField DataField="GoogleMapHtml" HeaderText="Google Map Html" SortExpression="[GoogleMapHtml]"  />
				<data:HyperLinkField HeaderText="Localidad Id" DataNavigateUrlFormatString="LocalidadEdit.aspx?Id={0}" DataNavigateUrlFields="Id" DataContainer="LocalidadIdSource" DataTextField="IdDepartamento" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Hotel Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnHotel" OnClientClick="javascript:location.href='HotelEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:HotelDataSource ID="HotelDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
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
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:HotelDataSource>
	    		
</asp:Content>



