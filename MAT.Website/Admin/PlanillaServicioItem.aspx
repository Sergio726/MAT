<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PlanillaServicioItem.aspx.cs" Inherits="PlanillaServicioItem" Title="PlanillaServicioItem List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Planilla Servicio Item List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PlanillaServicioItemDataSource"
				DataKeyNames="PlanillaServicioItemId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_PlanillaServicioItem.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PlanillaServicioItemId" HeaderText="Planilla Servicio Item Id" SortExpression="[PlanillaServicioItemID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Planilla Id" DataNavigateUrlFormatString="PlanillaEdit.aspx?PlanillaId={0}" DataNavigateUrlFields="PlanillaId" DataContainer="PlanillaIdSource" DataTextField="ViajeId" />
				<asp:BoundField DataField="ServicioId" HeaderText="Servicio Id" SortExpression="[ServicioID]"  />
				<asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="[Cantidad]"  />
				<asp:BoundField DataField="Subtotal" HeaderText="Subtotal" SortExpression="[Subtotal]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No PlanillaServicioItem Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPlanillaServicioItem" OnClientClick="javascript:location.href='PlanillaServicioItemEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PlanillaServicioItemDataSource ID="PlanillaServicioItemDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PlanillaServicioItemProperty Name="Planilla"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PlanillaServicioItemDataSource>
	    		
</asp:Content>



