<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PersonaEdit.aspx.cs" Inherits="PersonaEdit" Title="Persona Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Persona - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PersonaId" runat="server" DataSourceID="PersonaDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PersonaFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PersonaFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Persona not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PersonaDataSource ID="PersonaDataSource" runat="server"
			SelectMethod="GetByPersonaId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PersonaId" QueryStringField="PersonaId" Type="String" />

			</Parameters>
		</data:PersonaDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewReservaHabitacion1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewReservaHabitacion1_SelectedIndexChanged"			 			 
			DataSourceID="ReservaHabitacionDataSource1"
			DataKeyNames="ReservaHabitacionId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_ReservaHabitacion.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Habitacion Id" DataNavigateUrlFormatString="HabitacionEdit.aspx?HabitacionId={0}" DataNavigateUrlFields="HabitacionId" DataContainer="HabitacionIdSource" DataTextField="NroHabitacion" />
				<data:HyperLinkField HeaderText="Pasaje Id" DataNavigateUrlFormatString="PasajeEdit.aspx?PasajeId={0}" DataNavigateUrlFields="PasajeId" DataContainer="PasajeIdSource" DataTextField="FechaReserva" />
				<asp:BoundField DataField="FechaReserva" HeaderText="Fecha Reserva" SortExpression="[FechaReserva]" />				
				<asp:BoundField DataField="Desde" HeaderText="Desde" SortExpression="[Desde]" />				
				<asp:BoundField DataField="Hasta" HeaderText="Hasta" SortExpression="[Hasta]" />				
				<asp:BoundField DataField="Expiro" HeaderText="Expiro" SortExpression="[Expiro]" />				
				<asp:BoundField DataField="HoraIngreso" HeaderText="Hora Ingreso" SortExpression="[HoraIngreso]" />				
				<asp:BoundField DataField="HoraSalida" HeaderText="Hora Salida" SortExpression="[HoraSalida]" />				
				<data:HyperLinkField HeaderText="Pasajero Id" DataNavigateUrlFormatString="PersonaEdit.aspx?PersonaId={0}" DataNavigateUrlFields="PersonaId" DataContainer="PasajeroIdSource" DataTextField="Apellido" />
				<asp:BoundField DataField="ViajeId" HeaderText="Viaje Id" SortExpression="[ViajeID]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Reserva Habitacion Found! </b>
				<asp:HyperLink runat="server" ID="hypReservaHabitacion" NavigateUrl="~/admin/ReservaHabitacionEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:ReservaHabitacionDataSource ID="ReservaHabitacionDataSource1" runat="server" SelectMethod="Find"
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
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:ReservaHabitacionFilter  Column="PasajeroId" QueryStringField="PersonaId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ReservaHabitacionDataSource>		
		
		<br />
		

</asp:Content>

