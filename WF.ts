module WF {
   export module Template1 {
      export function render($dataContext : MyModel) : string {
         var $templateResult : string = '';
         $templateResult += "\r\n\t\t<div></div>\r\n\t";
         return $templateResult;
      }
   }
}
