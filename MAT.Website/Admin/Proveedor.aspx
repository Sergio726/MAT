<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Proveedor.aspx.cs" Inherits="Proveedor" Title="Proveedor List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Proveedor List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="ProveedorDataSource"
				DataKeyNames="ProveedorId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Proveedor.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<data:HyperLinkField HeaderText="Proveedor Id" DataNavigateUrlFormatString="PersonaEdit.aspx?PersonaId={0}" DataNavigateUrlFields="PersonaId" DataContainer="ProveedorIdSource" DataTextField="Apellido" />
				<asp:BoundField DataField="RazonSocial" HeaderText="Razon Social" SortExpression="[RazonSocial]"  />
				<asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="[Telefono]"  />
				<asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="[Fax]"  />
				<asp:BoundField DataField="Web" HeaderText="Web" SortExpression="[Web]"  />
				<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="[Email]"  />
				<asp:BoundField DataField="Idioma" HeaderText="Idioma" SortExpression="[Idioma]"  />
				<asp:BoundField DataField="CondicionIva" HeaderText="Condicion Iva" SortExpression="[CondicionIva]"  />
				<asp:BoundField DataField="Cuit" HeaderText="Cuit" SortExpression="[Cuit]"  />
				<asp:BoundField DataField="FormaPago" HeaderText="Forma Pago" SortExpression="[FormaPago]"  />
				<asp:BoundField DataField="LocalidadId" HeaderText="Localidad Id" SortExpression="[LocalidadID]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Proveedor Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnProveedor" OnClientClick="javascript:location.href='ProveedorEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:ProveedorDataSource ID="ProveedorDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ProveedorProperty Name="Persona"/> 
					<%--<data:ProveedorProperty Name="ServicioCollection" />--%>
					<%--<data:ProveedorProperty Name="ExcursionCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:ProveedorDataSource>
	    		
</asp:Content>



