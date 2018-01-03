# ifndef STACK_H
# define STACK_H

struct _stack
    {
      char oper;
      int idenf;
      double zhach;
      _stack* next;
    };

_stack* reverse_stack(_stack* s);
void del_elem(_stack*& b,_stack*& c1);
double elem(_stack*& stack);

_stack* add_oper(_stack* stack, char oper);
_stack* add_symbol(_stack* stack, double sym);

_stack* new_stack();
void free_stack(_stack*& stack);

# endif