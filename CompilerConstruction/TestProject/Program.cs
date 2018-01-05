using System;

namespace TestProject
{
	class BinaryTreeNode
	{
		BinaryTreeNode LeftNode = null;
		BinaryTreeNode RightNode = null;

		int Key = 0;
		int Value = 0;

		public BinaryTreeNode setValueAndKey(int key, int value)
		{
			Key = key;
			Value = value;
			return this;
		}

		public int getKey()
		{
			return Key;
		}

		public int getValue()
		{
			return Value;
		}

		public BinaryTreeNode setLeftNode(BinaryTreeNode leftNode)
		{
			LeftNode = leftNode;
			return LeftNode;
		}

		public BinaryTreeNode setRightNode(BinaryTreeNode rightNode)
		{
			RightNode = rightNode;
			return RightNode;
		}

		public BinaryTreeNode getLeftNode()
		{
			return LeftNode;
		}

		public BinaryTreeNode getRightNode()
		{
			return RightNode;
		}
	}

	class BinaryTree
	{
		BinaryTreeNode rootNode = null;

		public BinaryTreeNode insert_recursive(int key, int value,
			BinaryTreeNode currentNode, BinaryTreeNode parentNode, bool leftDir)
		{
			if (currentNode == null)
			{
				BinaryTreeNode node = new BinaryTreeNode();

				node.setValueAndKey(key, value);
				if (parentNode != null)
				{
					if (leftDir)
						parentNode.setLeftNode(node);
					else
						parentNode.setRightNode(node);
					return node;
				}
				else
				{
					rootNode = node;
					return node;
				}
			}
			else
				if (key >= currentNode.getKey())
					return insert_recursive(key, value, currentNode.getRightNode(), currentNode, false);
				else
					return insert_recursive(key, value, currentNode.getLeftNode(), currentNode, true);
		}

		public BinaryTreeNode insert(int key, int value)
		{
			return insert_recursive(key, value, rootNode, null, true);
		}

		public void PrintNode(BinaryTreeNode node)
		{
			Console.Write(node.getKey() + ":  ");

			var leftNode = node.getLeftNode();
			if (leftNode == null)
				Console.Write("null ");
			else
				Console.Write(leftNode.getKey() + "  ");

			var rightNode = node.getRightNode();
			if (rightNode == null)
				Console.Write("null ");
			else
				Console.Write(rightNode.getKey() + "  ");
			Console.WriteLine();

			if (leftNode != null)
				PrintNode(leftNode);
			if (rightNode != null)
				PrintNode(rightNode);
		}

		public void PrintTree()
		{
			PrintNode(rootNode);
		}
	}

	class A
	{

	}

	class B : A
	{

	}

	class C : B
	{

	}

	class Program
	{
		static int i = 0;
		static BinaryTree tree = new BinaryTree();
		static void Main()
		{
			var a = new A();
			if (a is C)
				i = 5;
			else if (a is B)
				i = 10;
			Console.WriteLine(i);
			/*tree.insert(5, 5);
			tree.insert(1, 1);
			tree.insert(10, 10);
			tree.PrintTree();
			i = Console.Read();
			*/
		}
	}
}
