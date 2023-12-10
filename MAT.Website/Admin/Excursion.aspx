<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Excursion.aspx.cs" Inherits="Excursion" Title="Excursion List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Excursion List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="ExcursionDataSource"
				DataKeyNames="ExcursionId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Excursion.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="ExcursionId" HeaderText="Excursion Id" SortExpression="[ExcursionID]" ReadOnly="True" />
				<asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="[Descripcion]"  />
				<asp:BoundField DataField="Costo" HeaderText="Costo" SortExpression="[Costo]"  />
				<asp:BoundField DataField="Observaciones" HeaderText="Observaciones" SortExpression="[Observaciones]"  />
				<data:HyperLinkField HeaderText="Proveedor Id" DataNavigateUrlFormatString="ProveedorEdit.aspx?ProveedorId={0}" DataNavigateUrlFields="ProveedorId" DataContainer="ProveedorIdSource" DataTextField="RazonSocial" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Excursion Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnExcursion" OnClientClick="javascript:location.href='ExcursionEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:ExcursionDataSource ID="ExcursionDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ExcursionProperty Name="Proveedor"/> 
					<%--<data:ExcursionProperty Name="PaqueteExcursionCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:ExcursionDataSource>
	    		
</asp:Content>



