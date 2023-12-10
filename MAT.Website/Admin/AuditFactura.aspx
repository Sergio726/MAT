<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="AuditFactura.aspx.cs" Inherits="AuditFactura" Title="AuditFactura List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Audit Factura List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="AuditFacturaDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_AuditFactura.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="FacturaId" HeaderText="Factura Id" SortExpression="[FacturaID]"  />
				<asp:BoundField DataField="PersonaId" HeaderText="Persona Id" SortExpression="[PersonaID]"  />
				<asp:BoundField DataField="VendedorId" HeaderText="Vendedor Id" SortExpression="[VendedorID]"  />
				<asp:BoundField DataField="Accion" HeaderText="Accion" SortExpression="[Accion]"  />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]"  />
				<asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha" SortExpression="[Fecha]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No AuditFactura Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnAuditFactura" OnClientClick="javascript:location.href='AuditFacturaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:AuditFacturaDataSource ID="AuditFacturaDataSource" runat="server"
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
		</data:AuditFacturaDataSource>
	    		
</asp:Content>



