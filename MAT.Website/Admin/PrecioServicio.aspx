<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PrecioServicio.aspx.cs" Inherits="PrecioServicio" Title="PrecioServicio List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Precio Servicio List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PrecioServicioDataSource"
				DataKeyNames="PrecioServicioId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_PrecioServicio.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PrecioServicioId" HeaderText="Precio Servicio Id" SortExpression="[PrecioServicioID]" ReadOnly="True" />
				<asp:BoundField DataField="ServicioId" HeaderText="Servicio Id" SortExpression="[ServicioID]"  />
				<asp:BoundField DataField="FechaRegistro" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Registro" SortExpression="[FechaRegistro]"  />
				<data:BoundRadioButtonField DataField="Activo" HeaderText="Activo" SortExpression="[Activo]"  />
				<asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="[Precio]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No PrecioServicio Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPrecioServicio" OnClientClick="javascript:location.href='PrecioServicioEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PrecioServicioDataSource ID="PrecioServicioDataSource" runat="server"
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
		</data:PrecioServicioDataSource>
	    		
</asp:Content>



