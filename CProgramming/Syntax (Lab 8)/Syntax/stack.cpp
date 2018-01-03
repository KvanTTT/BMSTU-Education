#include "stdafx.h"
#include "pol.h"
#include "stack.h"

_stack* reverse_stack(_stack* s)
{
  _stack *reversed = NULL;

  for(_stack* cur; s; s = s->next)
  {
    cur = new_stack();

    cur->idenf = s->idenf;
    cur->zhach = s->zhach;
    cur->oper  = s->oper;

    cur->next = reversed;
    reversed  = cur;
  }
  return reversed;
}
//-------------------------------------------------------
void del_elem(_stack*& b,_stack*& c1)
{
  b = c1->next;
  delete c1;
  c1 = b;
}
//-------------------------------------------------------
_stack* add_oper(_stack* stack, char oper)
{
  _stack *cur = new_stack();

  cur->oper = oper;
  cur->idenf = 1;
  cur->next = stack;
  stack = cur;

  return stack;
}
//-------------------------------------------------------
_stack* add_symbol(_stack* stack, double sym)
{
  _stack* cur=new_stack();

  cur->zhach = sym;
  cur->idenf = 0;
  cur->next = stack;
  stack = cur;

  return stack;
}
//-------------------------------------------------------
double elem(_stack*& stack)
{
  double a = stack->zhach;

  stack = stack->next;

  return a;
}
//-------------------------------------------------------
_stack* new_stack()
{
  _stack* new_stack = new _stack;
  
  return new_stack;
}
//-------------------------------------------------------
void free_stack(_stack*& stack)
{
  _stack* c;
  for(; stack->next; c->next = NULL)
  {
    for(c = stack; c->next->next==NULL; c = c->next);
    delete c->next;
  }
  delete stack;
}