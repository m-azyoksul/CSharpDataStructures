public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;

    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    public int MaxPathSum(TreeNode root)
    {
        (int singleChainMax, int doubleChainMax) = Foo(root);
        return doubleChainMax;
    }

    private static (int, int) Foo(TreeNode node)
    {
        if (node.left == null && node.right == null)
        {
            return (node.val, node.val);
        }
        else if (node.left == null)
        {
            (int rightSingleChainMax, int rightdoubleChainMax) = Foo(node.right);
            var singleChainMax = rightSingleChainMax > 0 ? rightSingleChainMax + node.val : node.val;
            var doubleChainMax = singleChainMax > rightdoubleChainMax ? singleChainMax : rightdoubleChainMax;

            return (singleChainMax, doubleChainMax);
        }
        else if (node.right == null)
        {
            (int leftSingleChainMax, int leftdoubleChainMax) = Foo(node.left);
            var singleChainMax = leftSingleChainMax > 0 ? leftSingleChainMax + node.val : node.val;
            var doubleChainMax = singleChainMax > leftdoubleChainMax ? singleChainMax : leftdoubleChainMax;

            return (singleChainMax, doubleChainMax);
        }
        else
        {
            (int rightSingleChainMax, int rightdoubleChainMax) = Foo(node.right);
            (int leftSingleChainMax, int leftdoubleChainMax) = Foo(node.left);

            var maxSingle = Max3(leftSingleChainMax, rightSingleChainMax, 0);
            var singleChainMax = maxSingle + node.val;

            var doubleChainMax = node.val;
            doubleChainMax += leftSingleChainMax > 0 ? leftSingleChainMax : 0;
            doubleChainMax += rightSingleChainMax > 0 ? rightSingleChainMax : 0;
            doubleChainMax = Max3(doubleChainMax, leftdoubleChainMax, rightdoubleChainMax);

            return (singleChainMax, doubleChainMax);
        }
    }

    private static int Max3(int a, int b, int c)
    {
        if (a > b)
        {
            if (a >= c)
                return a;
            else
                return c;
        }
        else
        {
            if (b >= c)
                return b;
            else
                return c;
        }
    }
}