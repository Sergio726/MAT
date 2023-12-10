<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Nota.aspx.cs" Inherits="Nota" Title="Nota List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Nota List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="NotaDataSource"
				DataKeyNames="NotaId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Nota.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="NotaId" HeaderText="Nota Id" SortExpression="[NotaID]" ReadOnly="True" />
				<asp:BoundField DataField="PorcentajeRetencion" HeaderText="Porcentaje Retencion" SortExpression="[PorcentajeRetencion]"  />
				<asp:BoundField DataField="MontoRetencion" HeaderText="Monto Retencion" SortExpression="[MontoRetencion]"  />
				<asp:BoundField DataField="Fecha" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha" SortExpression="[Fecha]"  />
				<asp:BoundField DataField="Dias" HeaderText="Dias" SortExpression="[Dias]"  />
				<data:HyperLinkField HeaderText="Cliente Id" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="ClienteIdSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="VendedorEdit.aspx?VendedorId={0}" DataNavigateUrlFields="VendedorId" DataContainer="VendedorIdSource" DataTextField="Descripcion" />
				<asp:BoundField DataField="NroNota" HeaderText="Nro Nota" SortExpression="[NroNota]"  />
				<asp:BoundField DataField="MontoNota" HeaderText="Monto Nota" SortExpression="[MontoNota]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Nota Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnNota" OnClientClick="javascript:location.href='NotaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:NotaDataSource ID="NotaDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:NotaProperty Name="Cliente"/> 
					<data:NotaProperty Name="Vendedor"/> 
					<%--<data:NotaProperty Name="MovimientoCuentaCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:NotaDataSource>
	    		
</asp:Content>



