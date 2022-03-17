export const PaginationFooter = {
    /**
     * Fit the colspan for the footer pagination 
     **/
    FitPaginationColSpam: (tableId) => {                
        try {
            const table = document.getElementById(tableId);
            const tbody = table.getElementsByTagName('tbody')[0];
            const myTableElements = tbody.getElementsByTagName('tr')[0];
            const footer = table.getElementsByTagName('tfoot')[0];
            const colspan = footer.getElementsByTagName('td')[0];
            colspan.setAttribute('colspan', myTableElements.children.length);
        } catch (e) { }            
    }
}