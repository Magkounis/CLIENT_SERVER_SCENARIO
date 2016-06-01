class C
{
public C() { val = 10; }

public int Value
{
    get { return val; }
    set { val = value; }
}
private int val;
}

//In the other two classes you need to have a reference to an instance (or more) of the class C:

class A
{
public A(C nc) { c = nc; }
public void DoSomething() { if(c) System.Diagnostics.Debug.WriteLine(c.Value); }

private C c;
}

class B
{
public B(C nc) { c = nc; }
public void DoSomething() { if(c) System.Diagnostics.Debug.WriteLine(c.Value); }

private C c;
}



C c = new C();
A a = new A(c);
B a = new B(c);

a.DoSomething();
b.DoSomething();