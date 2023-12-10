<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PlanillaEdit.aspx.cs" Inherits="PlanillaEdit" Title="Planilla Edit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Planilla - Add/Edit</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:MultiFormView ID="FormView1" DataKeyNames="PlanillaId" runat="server" DataSourceID="PlanillaDataSource">
		
			<EditItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PlanillaFields.ascx" />
			</EditItemTemplatePaths>
		
			<InsertItemTemplatePaths>
				<data:TemplatePath Path="~/Admin/UserControls/PlanillaFields.ascx" />
			</InsertItemTemplatePaths>
		
			<EmptyDataTemplate>
				<b>Planilla not found!</b>
			</EmptyDataTemplate>
			
			<FooterTemplate>
				<asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
				<asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
				<asp:Button ID="CancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
			</FooterTemplate>

		</data:MultiFormView>
		
		<data:PlanillaDataSource ID="PlanillaDataSource" runat="server"
			SelectMethod="GetByPlanillaId"
		>
			<Parameters>
				<asp:QueryStringParameter Name="PlanillaId" QueryStringField="PlanillaId" Type="String" />

			</Parameters>
		</data:PlanillaDataSource>
		
		<br />

		<data:EntityGridView ID="GridViewPlanillaServicioItem1" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPlanillaServicioItem1_SelectedIndexChanged"			 			 
			DataSourceID="PlanillaServicioItemDataSource1"
			DataKeyNames="PlanillaServicioItemId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PlanillaServicioItem.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Planilla Id" DataNavigateUrlFormatString="PlanillaEdit.aspx?PlanillaId={0}" DataNavigateUrlFields="PlanillaId" DataContainer="PlanillaIdSource" DataTextField="ViajeId" />
				<asp:BoundField DataField="ServicioId" HeaderText="Servicio Id" SortExpression="[ServicioID]" />				
				<asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="[Cantidad]" />				
				<asp:BoundField DataField="Subtotal" HeaderText="Subtotal" SortExpression="[Subtotal]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Planilla Servicio Item Found! </b>
				<asp:HyperLink runat="server" ID="hypPlanillaServicioItem" NavigateUrl="~/admin/PlanillaServicioItemEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PlanillaServicioItemDataSource ID="PlanillaServicioItemDataSource1" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PlanillaServicioItemProperty Name="Planilla"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PlanillaServicioItemFilter  Column="PlanillaId" QueryStringField="PlanillaId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PlanillaServicioItemDataSource>		
		
		<br />
		<data:EntityGridView ID="GridViewPlanillaHabitacionItem2" runat="server"
			AutoGenerateColumns="False"	
			OnSelectedIndexChanged="GridViewPlanillaHabitacionItem2_SelectedIndexChanged"			 			 
			DataSourceID="PlanillaHabitacionItemDataSource2"
			DataKeyNames="PlanillaHabitacionItemId"
			AllowMultiColumnSorting="false"
			DefaultSortColumnName="" 
			DefaultSortDirection="Ascending"	
			ExcelExportFileName="Export_PlanillaHabitacionItem.xls"  		
			Visible='<%# (FormView1.DefaultMode == FormViewMode.Insert) ? false : true %>'	
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" />
				<data:HyperLinkField HeaderText="Planilla Id" DataNavigateUrlFormatString="PlanillaEdit.aspx?PlanillaId={0}" DataNavigateUrlFields="PlanillaId" DataContainer="PlanillaIdSource" DataTextField="ViajeId" />
				<asp:BoundField DataField="HabitacionId" HeaderText="Habitacion Id" SortExpression="[HabitacionID]" />				
				<asp:BoundField DataField="Cantidad" HeaderText="Cantidad" SortExpression="[Cantidad]" />				
				<asp:BoundField DataField="Subtotal" HeaderText="Subtotal" SortExpression="[Subtotal]" />				
			</Columns>
			<EmptyDataTemplate>
				<b>No Planilla Habitacion Item Found! </b>
				<asp:HyperLink runat="server" ID="hypPlanillaHabitacionItem" NavigateUrl="~/admin/PlanillaHabitacionItemEdit.aspx">Add New</asp:HyperLink>
			</EmptyDataTemplate>
		</data:EntityGridView>					
		
		<data:PlanillaHabitacionItemDataSource ID="PlanillaHabitacionItemDataSource2" runat="server" SelectMethod="Find"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PlanillaHabitacionItemProperty Name="Planilla"/> 
				</Types>
			</DeepLoadProperties>
			
		    <Parameters>
				<data:SqlParameter Name="Parameters">
					<Filters>
						<data:PlanillaHabitacionItemFilter  Column="PlanillaId" QueryStringField="PlanillaId" /> 
					</Filters>
				</data:SqlParameter>
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" /> 
		    </Parameters>
		</data:PlanillaHabitacionItemDataSource>		
		
		<br />
		

</asp:Content>

