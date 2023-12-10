<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ViajeHotel.aspx.cs" Inherits="ViajeHotel" Title="ViajeHotel List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Viaje Hotel List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="ViajeHotelDataSource"
				DataKeyNames="ViajeHotelId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_ViajeHotel.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="ViajeHotelId" HeaderText="Viaje Hotel Id" SortExpression="[ViajeHotelID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Viaje Id" DataNavigateUrlFormatString="ViajeEdit.aspx?ViajeId={0}" DataNavigateUrlFields="ViajeId" DataContainer="ViajeIdSource" DataTextField="Origen" />
				<data:HyperLinkField HeaderText="Hotel Id" DataNavigateUrlFormatString="HotelEdit.aspx?HotelId={0}" DataNavigateUrlFields="HotelId" DataContainer="HotelIdSource" DataTextField="Nombre" />
				<asp:BoundField DataField="Desde" HeaderText="Desde" SortExpression="[Desde]"  />
				<asp:BoundField DataField="Hasta" HeaderText="Hasta" SortExpression="[Hasta]"  />
				<asp:BoundField DataField="HoraIngreso" HeaderText="Hora Ingreso" SortExpression="[HoraIngreso]"  />
				<asp:BoundField DataField="HoraSalida" HeaderText="Hora Salida" SortExpression="[HoraSalida]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No ViajeHotel Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnViajeHotel" OnClientClick="javascript:location.href='ViajeHotelEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:ViajeHotelDataSource ID="ViajeHotelDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ViajeHotelProperty Name="Hotel"/> 
					<data:ViajeHotelProperty Name="Viaje"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:ViajeHotelDataSource>
	    		
</asp:Content>



