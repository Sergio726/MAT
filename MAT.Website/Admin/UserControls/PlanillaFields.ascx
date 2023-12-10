<%@ Control Language="C#" ClassName="PlanillaFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPlanillaId" runat="server" Text="Planilla Id:" AssociatedControlID="dataPlanillaId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPlanillaId" Value='<%# Bind("PlanillaId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataViajeId" runat="server" Text="Viaje Id:" AssociatedControlID="dataViajeId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataViajeId" Value='<%# Bind("ViajeId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaRegistro" runat="server" Text="Fecha Registro:" AssociatedControlID="dataFechaRegistro" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaRegistro" Text='<%# Bind("FechaRegistro", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaRegistro" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataFechaRegistro" runat="server" Display="Dynamic" ControlToValidate="dataFechaRegistro" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTotal" runat="server" Text="Total:" AssociatedControlID="dataTotal" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTotal" Text='<%# Bind("Total") %>'></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataTotal" runat="server" Display="Dynamic" ControlToValidate="dataTotal" ErrorMessage="Required"></asp:RequiredFieldValidator><asp:RangeValidator ID="RangeVal_dataTotal" runat="server" Display="Dynamic" ControlToValidate="dataTotal" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


