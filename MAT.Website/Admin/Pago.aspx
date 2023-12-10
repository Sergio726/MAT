<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Pago.aspx.cs" Inherits="Pago" Title="Pago List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Pago List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PagoDataSource"
				DataKeyNames="PagoId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Pago.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PagoId" HeaderText="Pago Id" SortExpression="[PagoID]" ReadOnly="True" />
				<asp:BoundField DataField="FechaPago" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Pago" SortExpression="[FechaPago]"  />
				<asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="[Monto]"  />
				<asp:BoundField DataField="TipoPago" HeaderText="Tipo Pago" SortExpression="[TipoPago]"  />
				<asp:BoundField DataField="TransaccionId" HeaderText="Transaccion Id" SortExpression="[TransaccionID]"  />
				<data:HyperLinkField HeaderText="Cliente Id" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="ClienteIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="VendedorEdit.aspx?VendedorId={0}" DataNavigateUrlFields="VendedorId" DataContainer="VendedorIdSource" DataTextField="Descripcion" />
				<asp:BoundField DataField="NroRecibo" HeaderText="Nro Recibo" SortExpression="[NroRecibo]"  />
				<asp:BoundField DataField="EstadoRendicion" HeaderText="Estado Rendicion" SortExpression="[EstadoRendicion]"  />
				<data:HyperLinkField HeaderText="Cuenta Corriente Id" DataNavigateUrlFormatString="CuentaCorrienteEdit.aspx?CuentaCorrienteId={0}" DataNavigateUrlFields="CuentaCorrienteId" DataContainer="CuentaCorrienteIdSource" DataTextField="Fecha" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Pago Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPago" OnClientClick="javascript:location.href='PagoEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PagoDataSource ID="PagoDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PagoProperty Name="Cliente"/> 
					<data:PagoProperty Name="CuentaCorriente"/> 
					<data:PagoProperty Name="Vendedor"/> 
					<%--<data:PagoProperty Name="MovimientoCuentaCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PagoDataSource>
	    		
</asp:Content>



