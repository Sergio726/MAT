<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Ciudad.aspx.cs" Inherits="Ciudad" Title="Ciudad List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Ciudad List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="CiudadDataSource"
				DataKeyNames="CiudadId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Ciudad.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="CiudadId" HeaderText="Ciudad Id" SortExpression="[CiudadID]" ReadOnly="True" />
				<asp:BoundField DataField="CiudadNombre" HeaderText="Ciudad Nombre" SortExpression="[CiudadNombre]"  />
				<asp:BoundField DataField="PaisCodigo" HeaderText="Pais Codigo" SortExpression="[PaisCodigo]"  />
				<asp:BoundField DataField="CiudadDistrito" HeaderText="Ciudad Distrito" SortExpression="[CiudadDistrito]"  />
				<asp:BoundField DataField="CiudadPoblacion" HeaderText="Ciudad Poblacion" SortExpression="[CiudadPoblacion]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Ciudad Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnCiudad" OnClientClick="javascript:location.href='CiudadEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:CiudadDataSource ID="CiudadDataSource" runat="server"
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
		</data:CiudadDataSource>
	    		
</asp:Content>



