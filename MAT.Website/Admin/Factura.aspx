<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Factura.aspx.cs" Inherits="Factura" Title="Factura List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Factura List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="FacturaDataSource"
				DataKeyNames="FacturaId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Factura.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="FacturaId" HeaderText="Factura Id" SortExpression="[FacturaID]" ReadOnly="True" />
				<asp:BoundField DataField="NroFactura" HeaderText="Nro Factura" SortExpression="[NroFactura]"  />
				<asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="[Monto]"  />
				<asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha" SortExpression="[Fecha]"  />
				<asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="[Tipo]"  />
				<asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="[Estado]"  />
				<data:HyperLinkField HeaderText="Cliente Id" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="ClienteIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="VendedorEdit.aspx?VendedorId={0}" DataNavigateUrlFields="VendedorId" DataContainer="VendedorIdSource" DataTextField="Descripcion" />
				<asp:BoundField DataField="DescuentoAplicado" HeaderText="Descuento Aplicado" SortExpression="[DescuentoAplicado]"  />
				<asp:BoundField DataField="Observaciones" HeaderText="Observaciones" SortExpression="[Observaciones]"  />
				<asp:BoundField DataField="DiasPreReserva" HeaderText="Dias Pre Reserva" SortExpression="[DiasPreReserva]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Factura Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnFactura" OnClientClick="javascript:location.href='FacturaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:FacturaDataSource ID="FacturaDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:FacturaProperty Name="Cliente"/> 
					<data:FacturaProperty Name="Vendedor"/> 
					<%--<data:FacturaProperty Name="MovimientoCuentaCollection" />--%>
					<%--<data:FacturaProperty Name="PasajeCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:FacturaDataSource>
	    		
</asp:Content>



