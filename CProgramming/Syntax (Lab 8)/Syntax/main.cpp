#include "pol.hpp"

void main(void)
{
  clrscr();
  char* str = new char[MAX_LENGTH];
  strcpy(str,"26-r(15-10)+2-3*(1+2)*k(4)");
//  cout<<"Input value: ";
//  cin>>ss;
  cout<<str<<"="<<solve(str);
  getch();
  delete str;
}
