<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Planilla.aspx.cs" Inherits="Planilla" Title="Planilla List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Planilla List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PlanillaDataSource"
				DataKeyNames="PlanillaId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Planilla.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PlanillaId" HeaderText="Planilla Id" SortExpression="[PlanillaID]" ReadOnly="True" />
				<asp:BoundField DataField="ViajeId" HeaderText="Viaje Id" SortExpression="[ViajeID]"  />
				<asp:BoundField DataField="FechaRegistro" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Registro" SortExpression="[FechaRegistro]"  />
				<asp:BoundField DataField="Total" HeaderText="Total" SortExpression="[Total]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Planilla Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPlanilla" OnClientClick="javascript:location.href='PlanillaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PlanillaDataSource ID="PlanillaDataSource" runat="server"
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
		</data:PlanillaDataSource>
	    		
</asp:Content>



