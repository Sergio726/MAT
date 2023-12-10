<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PasajeEdit.aspx.cs" Inherits="PasajeEdit" Title="Pasaje Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Pasaje - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PasajeId" runat="server" DataSourceID="PasajeDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PasajeFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PasajeFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Pasaje not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PasajeDataSource ID="PasajeDataSource" runat="server"
			SelectMethod="GetByPasajeId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PasajeId" QueryStringField="PasajeId" Type="String" />

			</Parameters>
		</data:PasajeDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPasajeAdicional1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPasajeAdicional1_SelectedIndexChanged"			 			 
			DataSourceID="PasajeAdicionalDataSource1"
			DataKeyNames="PasajeAdicionalId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PasajeAdicional.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Pasaje Id" DataNavigateUrlFormatString="PasajeEdit.aspx?PasajeId={0}" DataNavigateUrlFields="PasajeId" DataContainer="PasajeIdSource" DataTextField="FechaReserva" />
				<data:HyperLinkField HeaderText="Adicional Id" DataNavigateUrlFormatString="AdicionalEdit.aspx?AdicionalId={0}" DataNavigateUrlFields="AdicionalId" DataContainer="AdicionalIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Pasaje Adicional Found! </b>
				<asp:HyperLink runat="server" ID="hypPasajeAdicional" NavigateUrl="~/admin/PasajeAdicionalEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PasajeAdicionalDataSource ID="PasajeAdicionalDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PasajeAdicionalProperty Name="Adicional"/> 
					<data:PasajeAdicionalProperty Name="Pasaje"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PasajeAdicionalFilter  Column="PasajeId" QueryStringField="PasajeId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PasajeAdicionalDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewReservaHabitacion2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewReservaHabitacion2_SelectedIndexChanged"			 			 
			DataSourceID="ReservaHabitacionDataSource2"
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
		
		<data:ReservaHabitacionDataSource ID="ReservaHabitacionDataSource2" runat="server" SelectMethod="Find"
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
						<data:ReservaHabitacionFilter  Column="PasajeId" QueryStringField="PasajeId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:ReservaHabitacionDataSource>		
		
		<br />
		

</asp:Content>

