<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Precio.aspx.cs" Inherits="Precio" Title="Precio List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Precio List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PrecioDataSource"
				DataKeyNames="PrecioId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Precio.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PrecioId" HeaderText="Precio Id" SortExpression="[PrecioID]" ReadOnly="True" />
				<asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="[Monto]"  />
				<asp:BoundField DataField="Vigencia" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Vigencia" SortExpression="[Vigencia]"  />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]"  />
				<asp:BoundField DataField="Mes" HeaderText="Mes" SortExpression="[Mes]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Precio Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPrecio" OnClientClick="javascript:location.href='PrecioEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PrecioDataSource ID="PrecioDataSource" runat="server"
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
		</data:PrecioDataSource>
	    		
</asp:Content>



