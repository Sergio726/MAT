<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="ReservaHabitacion.aspx.cs" Inherits="ReservaHabitacion" Title="ReservaHabitacion List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Reserva Habitacion List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="ReservaHabitacionDataSource"
				DataKeyNames="ReservaHabitacionId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_ReservaHabitacion.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="ReservaHabitacionId" HeaderText="Reserva Habitacion Id" SortExpression="[ReservaHabitacionID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Habitacion Id" DataNavigateUrlFormatString="HabitacionEdit.aspx?HabitacionId={0}" DataNavigateUrlFields="HabitacionId" DataContainer="HabitacionIdSource" DataTextField="NroHabitacion" />
				<data:HyperLinkField HeaderText="Pasaje Id" DataNavigateUrlFormatString="PasajeEdit.aspx?PasajeId={0}" DataNavigateUrlFields="PasajeId" DataContainer="PasajeIdSource" DataTextField="FechaReserva" />
				<asp:BoundField DataField="FechaReserva" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Reserva" SortExpression="[FechaReserva]"  />
				<asp:BoundField DataField="Desde" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Desde" SortExpression="[Desde]"  />
				<asp:BoundField DataField="Hasta" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Hasta" SortExpression="[Hasta]"  />
				<data:BoundRadioButtonField DataField="Expiro" HeaderText="Expiro" SortExpression="[Expiro]"  />
				<asp:BoundField DataField="HoraIngreso" HeaderText="Hora Ingreso" SortExpression="[HoraIngreso]"  />
				<asp:BoundField DataField="HoraSalida" HeaderText="Hora Salida" SortExpression="[HoraSalida]"  />
				<data:HyperLinkField HeaderText="Pasajero Id" DataNavigateUrlFormatString="PersonaEdit.aspx?PersonaId={0}" DataNavigateUrlFields="PersonaId" DataContainer="PasajeroIdSource" DataTextField="Apellido" />
				<asp:BoundField DataField="ViajeId" HeaderText="Viaje Id" SortExpression="[ViajeID]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No ReservaHabitacion Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnReservaHabitacion" OnClientClick="javascript:location.href='ReservaHabitacionEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:ReservaHabitacionDataSource ID="ReservaHabitacionDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:ReservaHabitacionProperty Name="Habitacion"/> 
					<data:ReservaHabitacionProperty Name="Pasaje"/> 
					<data:ReservaHabitacionProperty Name="Persona"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:ReservaHabitacionDataSource>
	    		
</asp:Content>



