var App = {};

    window.onload = Run;
    
    function Run(){
        App.input = $(".outputField");
        App.calculated = false;
    }

    function AddSymbol(symbol){
        var text = App.input.val();
        if(symbol === "."){
            var patt = new RegExp("\\d$");
            if(!patt.test(text)){
                text = text + "0";
            }
        }
        text = text + symbol;
        App.input.val(text);
    }

    function ClearAll(){
        App.input.val(String());
    }

    function Calculate(){
        var result = eval(App.input.val());
        AddSymbol("=" + result);
        App.calculated = true;
        App.result = result;
    }

    $(function(){
        $(".num").click(function(){
            var txt = $(this).text();
            if(App.calculated){
                ClearAll();
                App.calculated = false;
            }
            AddSymbol(txt);
        });
    })

    $(function(){
        $(".operator").click(function(){
            var txt = $(this).text();
            if(App.calculated){
                ClearAll();
                AddSymbol(App.result);
                App.calculated = false;
            }
            AddSymbol(txt);
        });
    })

    $(function(){
        $(".equals").click(function(){
            Calculate();
        });
    })
    $(function(){
        $(".clearBtn").click(function(){
            ClearAll();
            App.calculated = false;
        });
    })
