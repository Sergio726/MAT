<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="MovimientoCuenta.aspx.cs" Inherits="MovimientoCuenta" Title="MovimientoCuenta List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Movimiento Cuenta List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="MovimientoCuentaDataSource"
				DataKeyNames="MovimientoId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_MovimientoCuenta.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="MovimientoId" HeaderText="Movimiento Id" SortExpression="[MovimientoID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Pago Id" DataNavigateUrlFormatString="PagoEdit.aspx?PagoId={0}" DataNavigateUrlFields="PagoId" DataContainer="PagoIdSource" DataTextField="FechaPago" />
				<data:HyperLinkField HeaderText="Factura Id" DataNavigateUrlFormatString="FacturaEdit.aspx?FacturaId={0}" DataNavigateUrlFields="FacturaId" DataContainer="FacturaIdSource" DataTextField="NroFactura" />
				<asp:BoundField DataField="FechaRegistro" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Registro" SortExpression="[FechaRegistro]"  />
				<data:HyperLinkField HeaderText="Cuenta Id" DataNavigateUrlFormatString="CuentaEdit.aspx?CuentaId={0}" DataNavigateUrlFields="CuentaId" DataContainer="CuentaIdSource" DataTextField="Estado" />
				<data:HyperLinkField HeaderText="Nota Id" DataNavigateUrlFormatString="NotaEdit.aspx?NotaId={0}" DataNavigateUrlFields="NotaId" DataContainer="NotaIdSource" DataTextField="PorcentajeRetencion" />
				<data:HyperLinkField HeaderText="Cuenta Corriente Id" DataNavigateUrlFormatString="CuentaCorrienteEdit.aspx?CuentaCorrienteId={0}" DataNavigateUrlFields="CuentaCorrienteId" DataContainer="CuentaCorrienteIdSource" DataTextField="Fecha" />
				<data:HyperLinkField HeaderText="Debito Id" DataNavigateUrlFormatString="DebitoEdit.aspx?DebitoId={0}" DataNavigateUrlFields="DebitoId" DataContainer="DebitoIdSource" DataTextField="Fecha" />
			</Columns>
			<EmptyDataTemplate>
				<b>No MovimientoCuenta Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnMovimientoCuenta" OnClientClick="javascript:location.href='MovimientoCuentaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:MovimientoCuentaDataSource ID="MovimientoCuentaDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:MovimientoCuentaProperty Name="Cuenta"/> 
					<data:MovimientoCuentaProperty Name="CuentaCorriente"/> 
					<data:MovimientoCuentaProperty Name="Debito"/> 
					<data:MovimientoCuentaProperty Name="Factura"/> 
					<data:MovimientoCuentaProperty Name="Nota"/> 
					<data:MovimientoCuentaProperty Name="Pago"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:MovimientoCuentaDataSource>
	    		
</asp:Content>



