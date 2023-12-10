<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Cliente.aspx.cs" Inherits="Cliente" Title="Cliente List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Cliente List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="ClienteDataSource"
				DataKeyNames="ClienteId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Cliente.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="ClienteId" HeaderText="Cliente Id" SortExpression="[ClienteID]" ReadOnly="True" />
				<asp:BoundField DataField="RazonSocial" HeaderText="Razon Social" SortExpression="[RazonSocial]"  />
				<asp:BoundField DataField="Cuit" HeaderText="Cuit" SortExpression="[Cuit]"  />
				<asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="[Moneda]"  />
				<asp:BoundField DataField="Empresa" HeaderText="Empresa" SortExpression="[Empresa]"  />
				<asp:BoundField DataField="Ocupacion" HeaderText="Ocupacion" SortExpression="[Ocupacion]"  />
				<asp:BoundField DataField="FormaPago" HeaderText="Forma Pago" SortExpression="[FormaPago]"  />
				<asp:BoundField DataField="CondicionIva" HeaderText="Condicion Iva" SortExpression="[CondicionIva]"  />
				<asp:BoundField DataField="VendedorId" HeaderText="Vendedor Id" SortExpression="[VendedorID]"  />
				<asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="[Fax]"  />
				<asp:BoundField DataField="Web" HeaderText="Web" SortExpression="[Web]"  />
				<asp:BoundField DataField="Idioma" HeaderText="Idioma" SortExpression="[Idioma]"  />
				<asp:BoundField DataField="Promotor" HeaderText="Promotor" SortExpression="[Promotor]"  />
				<asp:BoundField DataField="Observacion" HeaderText="Observacion" SortExpression="[Observacion]"  />
				<asp:BoundField DataField="TipoId" HeaderText="Tipo Id" SortExpression="[TipoID]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Cliente Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnCliente" OnClientClick="javascript:location.href='ClienteEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:ClienteDataSource ID="ClienteDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
		>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:ClienteDataSource>
	    		
</asp:Content>



