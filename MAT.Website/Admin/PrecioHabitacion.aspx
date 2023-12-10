<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PrecioHabitacion.aspx.cs" Inherits="PrecioHabitacion" Title="PrecioHabitacion List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Precio Habitacion List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PrecioHabitacionDataSource"
				DataKeyNames="PrecioHabitacionId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_PrecioHabitacion.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PrecioHabitacionId" HeaderText="Precio Habitacion Id" SortExpression="[PrecioHabitacionID]" ReadOnly="True" />
				<asp:BoundField DataField="TipoHabitacion" HeaderText="Tipo Habitacion" SortExpression="[TipoHabitacion]"  />
				<asp:BoundField DataField="HotelId" HeaderText="Hotel Id" SortExpression="[HotelID]"  />
				<asp:BoundField DataField="FechaRegistro" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Registro" SortExpression="[FechaRegistro]"  />
				<data:BoundRadioButtonField DataField="Activo" HeaderText="Activo" SortExpression="[Activo]"  />
				<asp:BoundField DataField="Precio" HeaderText="Precio" SortExpression="[Precio]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No PrecioHabitacion Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPrecioHabitacion" OnClientClick="javascript:location.href='PrecioHabitacionEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PrecioHabitacionDataSource ID="PrecioHabitacionDataSource" runat="server"
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
		</data:PrecioHabitacionDataSource>
	    		
</asp:Content>



