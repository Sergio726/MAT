<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PaquetePrecio.aspx.cs" Inherits="PaquetePrecio" Title="PaquetePrecio List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Paquete Precio List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PaquetePrecioDataSource"
				DataKeyNames="PaquetePrecioId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_PaquetePrecio.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PaquetePrecioId" HeaderText="Paquete Precio Id" SortExpression="[PaquetePrecioID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Precio Id" DataNavigateUrlFormatString="PrecioEdit.aspx?PrecioId={0}" DataNavigateUrlFields="PrecioId" DataContainer="PrecioIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No PaquetePrecio Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPaquetePrecio" OnClientClick="javascript:location.href='PaquetePrecioEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PaquetePrecioDataSource ID="PaquetePrecioDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaquetePrecioProperty Name="Paquete"/> 
					<data:PaquetePrecioProperty Name="Precio"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PaquetePrecioDataSource>
	    		
</asp:Content>



