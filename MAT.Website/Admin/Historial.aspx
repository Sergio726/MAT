<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Historial.aspx.cs" Inherits="Historial" Title="Historial List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Historial List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="HistorialDataSource"
				DataKeyNames="HistorialId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Historial.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="HistorialId" HeaderText="Historial Id" SortExpression="[HistorialID]" ReadOnly="True" />
				<asp:BoundField DataField="Tabla" HeaderText="Tabla" SortExpression="[Tabla]"  />
				<asp:BoundField DataField="Operacion" HeaderText="Operacion" SortExpression="[Operacion]"  />
				<asp:BoundField DataField="FechaHoraRegistro" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Hora Registro" SortExpression="[FechaHoraRegistro]"  />
				<asp:BoundField DataField="Cliente" HeaderText="Cliente" SortExpression="[Cliente]"  />
				<asp:BoundField DataField="Vendedor" HeaderText="Vendedor" SortExpression="[Vendedor]"  />
				<asp:BoundField DataField="Observaciones" HeaderText="Observaciones" SortExpression="[Observaciones]"  />
				<asp:BoundField DataField="Monto" HeaderText="Monto" SortExpression="[Monto]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Historial Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnHistorial" OnClientClick="javascript:location.href='HistorialEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:HistorialDataSource ID="HistorialDataSource" runat="server"
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
		</data:HistorialDataSource>
	    		
</asp:Content>



