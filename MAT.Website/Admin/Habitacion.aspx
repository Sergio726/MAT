<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Habitacion.aspx.cs" Inherits="Habitacion" Title="Habitacion List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Habitacion List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="HabitacionDataSource"
				DataKeyNames="HabitacionId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Habitacion.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="HabitacionId" HeaderText="Habitacion Id" SortExpression="[HabitacionID]" ReadOnly="True" />
				<asp:BoundField DataField="NroHabitacion" HeaderText="Nro Habitacion" SortExpression="[NroHabitacion]"  />
				<asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="[Tipo]"  />
				<data:HyperLinkField HeaderText="Hotel Id" DataNavigateUrlFormatString="HotelEdit.aspx?HotelId={0}" DataNavigateUrlFields="HotelId" DataContainer="HotelIdSource" DataTextField="Nombre" />
				<asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="[Estado]"  />
				<asp:BoundField DataField="Capacidad" HeaderText="Capacidad" SortExpression="[Capacidad]"  />
				<asp:BoundField DataField="Ocupacion" HeaderText="Ocupacion" SortExpression="[Ocupacion]"  />
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="[Nombre]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Habitacion Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnHabitacion" OnClientClick="javascript:location.href='HabitacionEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:HabitacionDataSource ID="HabitacionDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:HabitacionProperty Name="Hotel"/> 
					<%--<data:HabitacionProperty Name="ReservaHabitacionCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:HabitacionDataSource>
	    		
</asp:Content>



